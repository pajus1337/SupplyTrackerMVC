using FluentValidation;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class NewAddressForSenderVm
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Country { get; set; }
    }

    public class NewAddressForSenderVmValidator : AbstractValidator<NewAddressForSenderVm>
    {
        public NewAddressForSenderVmValidator()
        {
            RuleFor(p => p.Street).Must(x => !string.IsNullOrWhiteSpace(x)).Length(3,100);
            RuleFor(p => p.City).Must(x => !string.IsNullOrWhiteSpace(x)).Length(2, 85);
            RuleFor(p => p.ZIP).Must(x => !string.IsNullOrWhiteSpace(x)).Length(3,10);
            RuleFor(p => p.Country).Must(x => !string.IsNullOrWhiteSpace(x)).Length(2, 56);
        }
    }
}


