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
            RuleFor(p => p.Street).Must(x => !string.IsNullOrWhiteSpace(x)).Length(3, 100);
            RuleFor(p => p.City).Must(x => !string.IsNullOrWhiteSpace(x)).Length(2, 85);
            RuleFor(p => p.ZIP).Must(x => !string.IsNullOrWhiteSpace(x)).Length(3, 10);
            RuleFor(p => p.Country).Must(x => !string.IsNullOrWhiteSpace(x)).Length(2, 56);
        }
    }
}
