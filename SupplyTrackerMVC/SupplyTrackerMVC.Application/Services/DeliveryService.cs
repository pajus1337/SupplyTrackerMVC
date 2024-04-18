using AutoMapper;
using FluentValidation.Results;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Interfaces;
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
        private readonly IMapper _mapper;

        public DeliveryService(IDeliveryRepository deliveryRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
        }
        public int AddNewDelivery(NewDeliveryVm model)
        {

            throw new NotImplementedException();
        }

        public DeliveryDetailsVm GetDeliveryDetailsById(int deliveryId)
        {
            var delivery = _deliveryRepository.GetDeliveryById(deliveryId);
            var deliveryVm = _mapper.Map(delivery, new DeliveryDetailsVm());

            return deliveryVm;
        }
    }
}
