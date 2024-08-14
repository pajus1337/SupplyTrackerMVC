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
    public class NewContactDetailVm : IMapFrom<ContactDetail>
    {
        public int Id { get; set; }
        [DisplayName("Select contact type")]
        public int SelectedContactDetailTypeId { get; set; }
        public ContactDetailTypeSelectListVm ContactDetailTypeSelectList { get; set; }
        [DisplayName("Value for selected contact type")]
        public string ContactDetailValue { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewContactDetailVm, ContactDetail>();
        }
    }

    public class NewContactDetailValidator : AbstractValidator<NewContactDetailVm>
    {
        public NewContactDetailValidator()
        {
            RuleFor(x => x.SelectedContactDetailTypeId).NotNull();
            RuleFor(x => x.ContactDetailValue).NotNull().Length(2, 20);
        }
    }
}
