using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ExternalServices.Email
{
    public sealed class SendmailOptions
    {
        public string Path { get; set; } = "/usr/sbin/sendmail";
        public string Args { get; set; } = "-t -i";
        [Required]
        public string? FromName { get; set; }
        [Required, EmailAddress]
        public string? FromEmail { get; set; }
    }
}
