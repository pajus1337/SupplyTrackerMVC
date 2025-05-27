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
    public class DeliveryForListVm : IMapFrom<Delivery>
    {
        public int Id { get; set; }
        public DateTime DeliveryDataTime { get; set; }
        public int ProductDeliveryWeight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delivery, DeliveryForListVm>();
        }
    }
}
