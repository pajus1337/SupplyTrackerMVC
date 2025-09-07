using Microsoft.Extensions.Options;
using MimeKit;
using SupplyTrackerMVC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.ExternalServices.Email
{
    public sealed class SendmailEmailSender : IEmailSender
    {
        private readonly SendmailOptions _opt;

        public SendmailEmailSender(IOptions<SendmailOptions> opt) => _opt = opt.Value;

        public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken cancellationToken)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_opt.FromName, _opt.FromEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(to));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new BodyBuilder
            {
                HtmlBody = htmlBody,
                TextBody = Regex.Replace(htmlBody, "<.*?>", string.Empty)
            }.ToMessageBody();

            using var memoryStream = new MemoryStream();
            await mimeMessage.WriteToAsync(memoryStream, cancellationToken);
            var raw = Encoding.UTF8.GetString(memoryStream.ToArray());

            var processStartInfo = new ProcessStartInfo
            {
                FileName = _opt.Path,
                Arguments = _opt.Args,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            using var process = new Process 
            { 
                StartInfo = processStartInfo 
            };

            if (!process.Start())
            {
                throw new InvalidOperationException("Cannot start a sendmail");
            }

            await process.StandardInput.WriteAsync(raw);
            process.StandardInput.Close();

            await process.WaitForExitAsync(cancellationToken);
            if (process.ExitCode != 0)
            {
                var error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"sendmail exit {process.ExitCode}: {error}");
            }
        }
    }
}
