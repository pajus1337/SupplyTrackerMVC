using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class DeliveryDetailsVm : IMapFrom<Delivery>
    {
        public int Id { get; set; }
        public DateTime DeliveryDataTime { get; set; }

        public int SenderId { get; set; }
        public string SenderName { get; set; }

        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverBranchName { get; set; }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductDeliveryWeight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delivery, DeliveryDetailsVm>()
                .ForMember(d => d.SenderName, opt => opt.MapFrom(s => s.Sender.Name))
                .ForMember(d => d.ReceiverName, opt => opt.MapFrom(s => s.Receiver.Name))
                // .ForMember(d => d.ReceiverBranchName, opt => opt.MapFrom(s => s.Receiver.DeliveryBranchs.)) => Add obj to Delivery.
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductDetail.ChemicalName))
                .ForMember(d => d.ProductDeliveryWeight, opt => opt.MapFrom(d => d.ProductDeliveryWeight));
        }
    }
}
