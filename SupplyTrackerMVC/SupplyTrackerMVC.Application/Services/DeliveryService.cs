using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Deliveries;


namespace SupplyTrackerMVC.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ISenderRepository _senderRepository;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFluentValidatorFactory _validatorFactory;
        private readonly IMapper _mapper;

        public DeliveryService(
            IFluentValidatorFactory validatorFactory,
            IDeliveryRepository deliveryRepository,
            ISenderRepository senderRepository,
            IReceiverRepository receiverRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _validatorFactory = validatorFactory;
            _deliveryRepository = deliveryRepository;
            _senderRepository = senderRepository;
            _receiverRepository = receiverRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ActionResponse<VoidValue>> AddNewDeliveryAsync(NewDeliveryVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewDeliveryVm>();
                var result = await validator.ValidateAsync(model, cancellationToken);
                if (!result.IsValid)
                {
                    return ActionResponse<VoidValue>.Failed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var delivery = _mapper.Map<Delivery>(model);
                var (isSuccess, deliveryId) = await _deliveryRepository.AddDeliveryAsync(delivery, cancellationToken);
                if (!isSuccess)
                {
                    return ActionResponse<VoidValue>.Failed(new string[] { "Failed to add new delivery" });
                }

                return ActionResponse<VoidValue>.Success(null, deliveryId);
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<DeliveryDetailsVm>> GetDeliveryDetailsByIdAsync(int deliveryId, CancellationToken cancellationToken)
        {
            if (deliveryId <= 0)
            {
                return ActionResponse<DeliveryDetailsVm>.Failed(new string[] { "Wrong delivery Id" });
            }

            var deliveryQuery = _deliveryRepository.GetDeliveryById(deliveryId).ProjectTo<DeliveryDetailsVm>(_mapper.ConfigurationProvider);

            try
            {
                var deliveryVm = await deliveryQuery.FirstOrDefaultAsync(cancellationToken);

                return ActionResponse<DeliveryDetailsVm>.Success(deliveryVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<DeliveryDetailsVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public NewDeliveryVm PrepareNewDeliveryViewModel()
        {
            var model = new NewDeliveryVm()
            {
                Senders = GetActiveSenders(),
                Receivers = GetActiveReceivers(),
                ReceiverBranches = GetActiveReceiverBranches(),
                Products = GetActiveProducts(),
            };

            return model;
        }

        private SenderSelectListVm GetActiveSenders() => new SenderSelectListVm()
        {
            Senders = _senderRepository.GetAllSenders().ProjectTo<SenderForSelectListVm>(_mapper.ConfigurationProvider)
        };

        private ReceiverSelectListVm GetActiveReceivers() => new ReceiverSelectListVm()
        {
            Receivers = _receiverRepository.GetAllReceivers().ProjectTo<ReceiverForSelectListVm>(_mapper.ConfigurationProvider)
        };

        private ReceiverBranchSelectListVm GetActiveReceiverBranches() => new ReceiverBranchSelectListVm
        {
            ReceiverBranches = _receiverRepository.GetAllReceiverBranches().ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider)
        };

        private ProductSelectListVm GetActiveProducts() => new ProductSelectListVm()
        {
            Products = _productRepository.GetAllProducts().ProjectTo<ProductForSelectListVm>(_mapper.ConfigurationProvider)
        };

        public ReceiverBranchSelectListVm GetReceiverBranchesByReceiverId(int receiverId) => new ReceiverBranchSelectListVm()
        {
            ReceiverBranches = _receiverRepository.GetAllReceiverBranches().Where(rb => rb.ReceiverId == receiverId).ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider)
        };

        // TODO : Finish implementation
        public async Task<ActionResponse<ListDeliveryForListVm>> GetDeliveryForListAsync(int pageSize, int pageNo, string searchBy, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var deliveriesQuery = _deliveryRepository.GetAllDeliveries();

                if (!string.IsNullOrWhiteSpace(searchString) && !string.IsNullOrWhiteSpace(searchBy))
                {
                    string searchLower = searchString.ToLower();

                    deliveriesQuery = searchBy switch
                    {
                        "Sender" => deliveriesQuery.Where(p => p.Sender.Name.ToLower().StartsWith(searchLower)),
                        "Receiver" => deliveriesQuery.Where(p => p.Receiver.Name.ToLower().StartsWith(searchLower)),
                        "ReceiverBranch" => deliveriesQuery.Where(p => p.Receiver.ReceiverBranches.Any(rb => rb.Name.ToLower().StartsWith(searchLower))),
                        "Product" => deliveriesQuery.Where(p => p.Product.Name.ToLower().StartsWith(searchLower)),
                        _ => deliveriesQuery
                    };
                }

                deliveriesQuery = deliveriesQuery.OrderBy(p => p.Id);

                var totalCount = await deliveriesQuery.CountAsync(cancellationToken);

                var deliveriesToShow = await deliveriesQuery
                    .Skip(pageSize * (pageNo - 1))
                    .Take(pageSize)
                    .ProjectTo<DeliveryForListVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var result = new ListDeliveryForListVm()
                {
                    Deliveries = deliveriesToShow,
                    CurrentPage = pageNo,
                    PageSize = pageSize,
                    SearchBy = searchBy,
                    SearchString = searchString,
                    Count = totalCount,
                };

                return ActionResponse<ListDeliveryForListVm>.Success(result);
            }
            catch (Exception ex)
            {
                return ActionResponse<ListDeliveryForListVm>.Failed(new string[] { "An error occurred while getting deliveries", ex.Message });
            }
        }
    }
}
