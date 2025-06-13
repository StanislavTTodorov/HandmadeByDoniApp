using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);

        string GetConfirmOrderEmail(UserOrder userOrder);
        string GetConfirmEmail(string token, ApplicationUser user);
        string GetCancellationOrderEmail(UserOrder userOrder);
        string GetOrderSentEmail(UserOrder userOrder);
    }
}
