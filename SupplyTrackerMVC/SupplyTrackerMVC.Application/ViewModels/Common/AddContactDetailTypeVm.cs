using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class AddContactDetailTypeVm : IMapFrom<ContactDetailType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddContactDetailTypeVm, ContactDetailType>();
        }
    }
    public class AddContactDetailTypeValidator : AbstractValidator<AddContactDetailTypeVm>
    {
        public AddContactDetailTypeValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(3,12);
        }
    }
}
