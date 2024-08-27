﻿using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SupplyTrackerMVC.Application.ViewModels.SenderVm.NewContactForSenderVm;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class UpdateSenderVm : IMapFrom<Sender>
    {
        // TODO: Finish VM for Update Sender.
        public int Id { get; set; }
        public string Name { get; set; }
        public AddressDetailsVm Address { get; set; }
        public ICollection<ContactDetailsVm> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSenderVm, Sender>().ReverseMap();
            profile.CreateMap<ContactDetailsVm, Contact>().ReverseMap();
            profile.CreateMap<AddressDetailsVm, Address>().ReverseMap();
        }
    }
    public class UpdateSenderValidator : AbstractValidator<UpdateSenderVm>
    {
        public UpdateSenderValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 5);
            RuleFor(x => x.Address).SetValidator(new NewAddressForSenderVmValidator());
            RuleFor(x => x.Contacts).SetValidator(new NewContactForSenderVmValidator());
        }
    }
}
