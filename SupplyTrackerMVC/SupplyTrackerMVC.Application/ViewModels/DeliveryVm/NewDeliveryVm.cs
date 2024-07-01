using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class NewDeliveryVm : IMapFrom<Delivery>
    {
        public int Id { get; set; }
        public DateTime DeliveryDataTime { get; set; } = DateTime.Now;
        public int SelectedSenderId { get; set; }
        public SenderSelectListVm Senders { get; set; }
        public int SelectedReceiverId { get; set; }
        public ReceiverSelectListVm Receivers { get; set; }
        public int SelectedReceiverBranchId { get; set; }
        public ReceiverBranchSelectListVm ReceiverBranches { get; set; }
        public int SelectedProductId { get; set; }
        public ProductSelectListVm Products { get; set; }

        public int ProductDeliveryWeight { get; set; }


        // TODO: Add Mapping
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewDeliveryVm, Delivery>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.SelectedProductId))
                .ForMember(d => d.ReceiverId, opt => opt.MapFrom(s => s.SelectedReceiverId))
                .ForMember(d => d.SenderId, opt => opt.MapFrom(s => s.SelectedSenderId));
        }

        public class NewDeliveryValidator : AbstractValidator<NewDeliveryVm>
        {
            public NewDeliveryValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.DeliveryDataTime).GreaterThanOrEqualTo(DateTime.Today);
            }
        }
    }
}
