﻿using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class UpdateContactDetailVm : IMapFrom<ContactDetail>
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int ContactDetailTypeId { get; set; }
        public string ContactDetailTypeName { get; set; }
        public string ContactDetailValue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDetail, UpdateContactDetailVm>()
                .ForMember(dest => dest.ContactDetailTypeName, opt => opt.MapFrom(src => src.ContactDetailType.Name)).ReverseMap();
        }
    }

    public class UpdateContactDetailValidator : AbstractValidator<UpdateContactDetailVm>
    {
        public UpdateContactDetailValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.ContactId).GreaterThan(0);
            RuleFor(x => x.ContactDetailTypeId).GreaterThan(0);
            RuleFor(x => x.ContactDetailTypeName).NotEmpty();
            RuleFor(x => x.ContactDetailValue).Length(1, 20);
        }
    }
}
