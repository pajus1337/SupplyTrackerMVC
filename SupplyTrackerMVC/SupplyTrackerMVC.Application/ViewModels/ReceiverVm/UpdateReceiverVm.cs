using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Receivers;
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Receiver, UpdateReceiverVm>();
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
