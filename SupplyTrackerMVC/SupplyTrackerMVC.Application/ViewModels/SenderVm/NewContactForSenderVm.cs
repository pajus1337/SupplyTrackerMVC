
using FluentValidation;
using SupplyTrackerMVC.Domain.Model.Contacts;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class NewContactForSenderVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public ICollection<ContactDetail> ContactDetails { get; set; }
        public int SenderId { get; set; }

        public class NewContactForSenderVmValidator : AbstractValidator<NewContactForSenderVm>
        {
            public NewContactForSenderVmValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.FirstName).MinimumLength(2).MaximumLength(100);
                RuleFor(x => x.LastName).MinimumLength(2).MaximumLength(100);
                RuleFor(x => x.Role).MinimumLength(2).MaximumLength(100);
                RuleFor(x => x.ContactDetails).NotEmpty();
            }
        }
    }
}
