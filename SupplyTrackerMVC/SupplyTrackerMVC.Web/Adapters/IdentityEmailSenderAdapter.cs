using IdentityEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;
using AppEmailSender = SupplyTrackerMVC.Application.Interfaces.IEmailSender;

namespace SupplyTrackerMVC.Web.Adapters
{
    public sealed class IdentityEmailSenderAdapter : IdentityEmailSender
    {
        private readonly AppEmailSender _inner;

        public IdentityEmailSenderAdapter(AppEmailSender inner)
        {
            _inner = inner;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage) => _inner.SendAsync(email, subject, htmlMessage);
    }
}
