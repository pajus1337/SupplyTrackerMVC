using FluentValidation;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class NewAddressForReceiverVm
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
    }

    public class NewAddressForReceiverVmValidator : AbstractValidator<NewAddressForReceiverVm>
    {
        public NewAddressForReceiverVmValidator()
        {
            RuleFor(p => p.Street).NotNull().MinimumLength(3);
            RuleFor(p => p.City).NotNull().MinimumLength(3);
            RuleFor(p => p.ZIP).NotNull().MinimumLength(3);
            RuleFor(p => p.Country).NotNull().MinimumLength(3);
        }
    }
}
