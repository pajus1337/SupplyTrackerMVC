using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken);
        Task SendAsync(string to, string subject, string htmlBody) => SendAsync(to, subject, htmlBody, CancellationToken.None);
    }
}
