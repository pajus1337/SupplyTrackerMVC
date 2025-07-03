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

        public DeliveryService(IFluentValidatorFactory validatorFactory, IDeliveryRepository deliveryRepository, ISenderRepository senderRepository, IReceiverRepository receiverRepository, IProductRepository productRepository, IMapper mapper)
        {
            _validatorFactory = validatorFactory;
            _deliveryRepository = deliveryRepository;
            _senderRepository = senderRepository;
            _receiverRepository = receiverRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<VoidValue>> AddNewDeliveryAsync(NewDeliveryVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var validator = _validatorFactory.GetValidator<NewDeliveryVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return ServiceResponse<VoidValue>.CreateFailed(result.Errors.Select(e => e.ErrorMessage), true);
            }

            var delivery = _mapper.Map<Delivery>(model);
            try
            {
                var (isSuccess, delieryId) = await _deliveryRepository.AddDeliveryAsync(delivery, cancellationToken);

                if (!isSuccess)
                {
                    return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Failed to add new delivery" });
                }

                return ServiceResponse<VoidValue>.CreateSuccess(null, delieryId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<DeliveryDetailsVm>> GetDeliveryDetailsByIdAsync(int deliveryId, CancellationToken cancellationToken)
        {
            if (deliveryId <= 0)
            {
                return ServiceResponse<DeliveryDetailsVm>.CreateFailed(new string[] { "Wrong delivery Id" });
            }

            var deliveryQuery = _deliveryRepository.GetDeliveryById(deliveryId).ProjectTo<DeliveryDetailsVm>(_mapper.ConfigurationProvider);

            try
            {
                var deliveryVm = await deliveryQuery.FirstOrDefaultAsync(cancellationToken);

                return ServiceResponse<DeliveryDetailsVm>.CreateSuccess(deliveryVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<DeliveryDetailsVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
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

        public async Task<ServiceResponse<ListDeliveryForListVm>> GetDeliveryForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var deliveriesQuery = _deliveryRepository.GetAllDeliveries().Where(p => p.Receiver.Name.StartsWith(searchString)).OrderBy(p => p.Id);
                var deliveriesToShow = await deliveriesQuery.Skip(pageSize * (pageNo - 1)).Take(pageSize).ProjectTo<DeliveryForListVm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                var result = new ListDeliveryForListVm()
                {
                    Deliveries = deliveriesToShow,
                    CurrentPage = pageNo,
                    PageSize = pageSize,
                    SearchString = searchString,
                    Count = deliveriesQuery.Count()
                };

                return ServiceResponse<ListDeliveryForListVm>.CreateSuccess(result);
            }      
            catch (Exception ex)
            {
                return ServiceResponse<ListDeliveryForListVm>.CreateFailed(new string[] { "An error occurred while getting deliveries", ex.Message});
            }
        }
    }
}
