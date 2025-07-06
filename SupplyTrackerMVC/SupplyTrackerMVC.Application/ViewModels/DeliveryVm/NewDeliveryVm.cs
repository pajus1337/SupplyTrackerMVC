﻿using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Deliveries;

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewDeliveryVm, Delivery>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.SelectedProductId))
                .ForMember(d => d.ReceiverId, opt => opt.MapFrom(s => s.SelectedReceiverId))
                .ForMember(d => d.SenderId, opt => opt.MapFrom(s => s.SelectedSenderId))
                .ForMember(d => d.ReceiverBranchId, opt => opt.MapFrom(s => s.SelectedReceiverBranchId));
        }

        public class NewDeliveryValidator : AbstractValidator<NewDeliveryVm>
        {
            public NewDeliveryValidator()
            {
                RuleFor(x => x.SelectedSenderId).GreaterThan(0);
                RuleFor(x => x.SelectedReceiverId).GreaterThan(0);
                RuleFor(x => x.SelectedReceiverBranchId).GreaterThan(0);
                RuleFor(x => x.SelectedProductId).GreaterThan(0);
                RuleFor(x => x.DeliveryDataTime).GreaterThanOrEqualTo(DateTime.Today);
            }
        }
    }
}
