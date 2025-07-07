using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReportVm
{
    public class ReportDeliveryVm : IMapFrom<Delivery>
    {
        public int Id { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverBranchName { get; set; }
        public string ProductName { get; set; }
        public int ProductDeliveryWeight { get; set; }
        public DateTime DeliveryDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Delivery, ReportDeliveryVm>();
        }
    }
}
