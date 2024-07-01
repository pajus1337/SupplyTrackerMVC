using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ISenderRepository _senderRepository;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeliveryService(IDeliveryRepository deliveryRepository, ISenderRepository senderRepository, IReceiverRepository receiverRepository, IProductRepository productRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _senderRepository = senderRepository;
            _receiverRepository = receiverRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task<ServiceResponse<VoidValue>> AddNewDeliveryAsync(NewDeliveryVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public DeliveryDetailsVm GetDeliveryDetailsById(int deliveryId)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);
            var deliveryVm = _mapper.Map(delivery, new DeliveryDetailsVm());

            return deliveryVm;
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

        public async Task<ServiceResponse<ListDeliveryForListVm>> GetDeliveryForListAsync(CancellationToken cancellationToken)
        {
            var deliveriesQuery = _deliveryRepository.GetAllDeliveries().ProjectTo<DeliveryForListVm>(_mapper.ConfigurationProvider);

            try
            {
                var deliveries = await deliveriesQuery.ToListAsync(cancellationToken);
                if (deliveries.Count > 0)
                {
                    var result = new ListDeliveryForListVm()
                    {
                        Deliveries = deliveries,
                        Count = deliveries.Count
                    };

                    return ServiceResponse<ListDeliveryForListVm>.CreateSuccess(result);
                }

                return ServiceResponse<ListDeliveryForListVm>.CreateFailed(new string[] { "There are no deliveries in Db" });
            }
            catch (Exception ex)
            {
                return ServiceResponse<ListDeliveryForListVm>.CreateFailed(new string[] { "An error occurred while getting deliveries", ex.Message });
            }
        }
    }
}
