using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.Common
{
    public class AddContactVm : IMapFrom<Contact>
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Role { get; set; }
        public int ContactOwnerId { get; set; }

        public AddContactDetailVm ContactDetailVm { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddContactVm, Contact>()
                .ForMember(dest => dest.ReceiverId, opt => opt.Ignore())
                .ForMember(dest => dest.SenderId, opt => opt.Ignore());
        }
    }

    public class AddContactValidator : AbstractValidator<AddContactVm>
    {
        public AddContactValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.ContactOwnerId).NotNull().GreaterThan(0);
            RuleFor(x => x.ContactDetailVm).SetValidator(new AddContactDetailValidator());
        }
    }
}
