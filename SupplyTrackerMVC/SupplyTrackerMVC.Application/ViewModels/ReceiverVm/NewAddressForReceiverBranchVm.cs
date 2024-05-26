using FluentValidation;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class NewAddressForReceiverBranchVm
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
    }

    public class NewAddressForReceiverBranchVmValidator : AbstractValidator<NewAddressForReceiverBranchVm>
    {
        public NewAddressForReceiverBranchVmValidator()
        {
            RuleFor(p => p.Street).NotNull().MinimumLength(3);
            RuleFor(p => p.City).NotNull().MinimumLength(3);
            RuleFor(p => p.ZIP).NotNull().MinimumLength(3);
            RuleFor(p => p.Country).NotNull().MinimumLength(3);
        }
    }
}
