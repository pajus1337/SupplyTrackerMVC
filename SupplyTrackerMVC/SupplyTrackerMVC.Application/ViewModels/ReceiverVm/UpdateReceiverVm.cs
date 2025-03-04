using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Addresses;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    // HACK: Of updateReceiver , Read below -> TODO.
    // TODO: Refine this class, add prop. and adjust mapping and validatior for real scenario.
    public class UpdateReceiverVm : IMapFrom<Receiver>
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public UpdateAddressDetailsVm Address { get; set; }
        public ICollection<UpdateContactVm> Contacts { get; set; }
        public ICollection<ReceiverBranchForListVm> ReceiverBranches { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Receiver, UpdateReceiverVm>()
                .ForMember(d => d.ReceiverBranches, opt => opt.MapFrom(s => s.ReceiverBranches));
            profile.CreateMap<UpdateAddressDetailsVm, Address>().ReverseMap();
            profile.CreateMap<UpdateContactVm, Contact>().ReverseMap();
        }

    }

    public class UpdateReceiverValidator : AbstractValidator<UpdateReceiverVm>
    {
        public UpdateReceiverValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
