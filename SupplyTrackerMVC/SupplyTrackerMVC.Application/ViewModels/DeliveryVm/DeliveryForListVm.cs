using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class DeliveryForListVm : IMapFrom<Delivery>
    {
        public int Id { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public int ProductDeliveryWeight { get; set; }
        public string ProductName { get; set; }
        public string SenderName { get; set; }
        public string ReceiverBranchName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delivery, DeliveryForListVm>()
                .ForMember(destination => destination.ReceiverBranchName, opt => opt.MapFrom(source => source.ReceiverBranch.Name));
        }
    }
}
