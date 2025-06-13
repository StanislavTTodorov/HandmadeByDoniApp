﻿using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.User;
using Resources.Resources;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MimeKit;
//using Resources.Resources;
using static HandmadeByDoniApp.Common.GeneralMessages;
using System.Text;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class EmailService:IEmailService
    {
        private readonly IRepository repository;

        private readonly ILogger<EmailService> logger;

        private readonly IStringLocalizer<App> L;


        public EmailService(IRepository repository, ILogger<EmailService> logger, IStringLocalizer<App> localizer)
        {
            this.repository = repository;
            this.logger = logger;
            this.L = localizer;
        }
        private async Task<bool> SendAdminEmailAsync(string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(Name, AutoSentEmail));
                message.To.Add(MailboxAddress.Parse(AdminEmail));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(AutoSentEmail, AutoSentPassword);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"SendAdminEmailAsync error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(Name, AutoSentEmail));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = L[subject];

                message.Body = new TextPart("html") { Text = body };

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                /*
                     Условия за достъп до App Passwords:
                    Двуфакторна автентикация (2FA) е включена в Google акаунта ти:

                    Отиди на: https://myaccount.google.com/security

                    Превърти до секцията "Signing in to Google"

                    Активирай 2-Step Verification (Двуфакторно удостоверяване)

                    След като активираш 2FA, се връщаш на:
                    👉 https://myaccount.google.com/apppasswords

                    Там можеш да:

                    Избереш „Mail“

                    Избереш устройство (или напиши „Custom name“)

                    Натисни "Generate"

                    Копирай 16-символната парола — това е паролата, която използваш в твоя C# код (вместо истинската си Gmail парола)
                 */
                // Използвай App Password ако имаш включена 2FA
                await client.AuthenticateAsync(AutoSentEmail, AutoSentPassword);

                await client.SendAsync(message);
               
                await client.DisconnectAsync(true); 

                if (subject != "ConfirmEmail")
                {
                    await SendAdminEmailAsync(message.Subject, body);
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"SendEmailAsync error: {ex.Message}");
                return false;
            }       
        }

        public string GetConfirmOrderEmail(UserOrder userOrder)
        {
            var fullName = $"{userOrder.User.FirstName} {userOrder.User.LastName}";
            var address = userOrder.Address;
            var order = userOrder.Order;
            var orderDate = userOrder.CreaateOn.ToString("dd.MM.yyyy");
            var deliveryCompany = address.DeliveryCompany.Name;
            var paymentMethod = address.MethodPayment.Method;
            var shipmentNote = !string.IsNullOrEmpty(userOrder.ShipmentNoteNumber)
                ? $"<p><strong>{L["ShipmentNoteNumber"]}</strong> {userOrder.ShipmentNoteNumber}</p>"
                : string.Empty;

            var productsHtml = "";
            foreach (var product in order.Products)
            {
                productsHtml += $@"
                <tr>
                    <td style='padding: 8px; border: 1px solid #ccc;'>{product.Title}</td>
                    <td style='padding: 8px; border: 1px solid #ccc;'>{product.Price:C}</td>
                </tr>";
            }

            string emailBody = $@"
                    <!DOCTYPE html>
                    <html lang='bg'>
                    <head>
                        <meta charset='UTF-8'>
                        <title>{L["ConfirmOrderEmailTitle"]}</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                        <h2 style='color: #0066cc;'>{L["ConfirmOrderEmailHeading"]}</h2>
                        <p>{L["Greeting"]}, <strong>{fullName}</strong>,</p>

                        <p>{L["ThankYouForOrder"]}</p>

                        <h3>{L["OrderDetailsHeading"]}</h3>
                        <p><strong>{L["OrderNumberLabel"]}</strong> {userOrder.OrderId}</p>
                        <p><strong>{L["OrderDateLabel"]}</strong> {orderDate}</p>
                        <p><strong>{L["TotalPriceLabel"]}</strong> {userOrder.TotalPrice:C}</p>
                        <p><strong>{L["Status"]}</strong> {(userOrder.IsSent ? L["OrderSent"] : L["OrderPreparing"])}</p>
                        {shipmentNote}

                        <h3>{L["PurchasedProductsHeading"]}</h3>
                        <table style='width: 100%; border-collapse: collapse;'>
                            <thead>
                                <tr>
                                    <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>{L["Product"]}</th>
                                    <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>{L["Price"]}</th>
                                </tr>
                            </thead>
                            <tbody>
                                {productsHtml}
                            </tbody>
                        </table>

                        <h3>{L["DeliveryDetailsHeading"]}</h3>
                        <p><strong>{L["Address"]}</strong> {address.Street}, {address.CityName}, {address.CountryName}</p>
                        <p><strong>{L["ContactPhone"]}</strong> {address.PhoneNumber}</p>
                        <p><strong>{L["Courier"]}</strong> {deliveryCompany}</p>
                        <p><strong>{L["MethodPayment"]}</strong> {paymentMethod}</p>

                        <br>
                        <p>{L["QuestionsContactUs"]}</p>

                        <p>{L["Regards"]},<br><strong>{L["HandmadeByDoniTeam"]}</strong></p>
                    </body>
                    </html>
";

            return emailBody;
        }

        public string GetConfirmEmail(string token, ApplicationUser user)
        {
#if DEBUG
            string confirmationLink = $"https://localhost:7142/User/ConfirmEmail?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";
#else
            string confirmationLink = $"https://185.89.126.217:8080/User/ConfirmEmail?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";
#endif

            string emailBody = $@"
                            <!DOCTYPE html>
                            <html lang='bg'>
                            <head>
                                <meta charset='UTF-8'>
                                    <title>{L["ConfirmEmailTitle"]}</title>
                            </head>
                            <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                                    <h2 style='color: #0066cc;'>{string.Format(L["WelcomeUser"], user.FirstName)}</h2>
                                    <p>{L["ThankYouForRegistering"]}</p>

                                    <p>{L["PleaseConfirmEmail"]}</p>

                                <p style='margin: 30px 0;'>
                                    <a href='{confirmationLink}' style='padding: 10px 20px; background-color: #28a745; color: #fff;
                                        text-decoration: none; border-radius: 5px;'>{L["ConfirmEmailButton"]}</a>
                                </p>

                                    <p>{L["IgnoreIfNotRegistered"]}</p>

                                    <p>{L["Regards"]},<br><strong>{L["HandmadeByDoniTeam"]}</strong></p>
                            </body>
                                </html>
                                ";

            return emailBody;
        }

        public string GetCancellationOrderEmail(UserOrder userOrder)
        {
            var fullName = $"{userOrder.User.FirstName} {userOrder.User.LastName}";
            var address = userOrder.Address;
            var order = userOrder.Order;
            var orderDate = userOrder.CreaateOn.ToString("dd.MM.yyyy");
            var cancellationDate = DateTime.Now.ToString("dd.MM.yyyy");

            var productsHtml = "";
            foreach (var product in order.Products)
            {
                productsHtml += $@"
                                <tr>
                                    <td style='padding: 8px; border: 1px solid #ccc;'>{product.Title}</td>
                                    <td style='padding: 8px; border: 1px solid #ccc;'>{product.Price:C}</td>
                                </tr>";
            }

            string emailBody = $@"
                                <!DOCTYPE html>
                                <html lang='bg'>
                                <head>
                                    <meta charset='UTF-8'>
                                    <title>{L["CancellationOrderEmailTitle"]}</title>
                                </head>
                                <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                                    <h2 style='color: #0066cc;'>{L["CancellationOrderEmailHeading"]}</h2>
                                    <p>{L["Greeting"]}, <strong>{fullName}</strong>,</p>

                                    <p>{L["SorryForCancellation"]}</p>

                                    <h3>{L["OrderDetailsHeading"]}</h3>
                                    <p><strong>{L["OrderNumberLabel"]}</strong> {userOrder.OrderId}</p>
                                    <p><strong>{L["OrderDateLabel"]}</strong> {orderDate}</p>
                                    <p><strong>{L["CancellationDateLabel"]}</strong> {cancellationDate}</p>
                                    <p><strong>{L["TotalPriceLabel"]}</strong> {userOrder.TotalPrice:C}</p>
                                    <p><strong>{L["Status"]}</strong> {L["OrderCancelled"]}</p>

                                    <h3>{L["PurchasedProductsHeading"]}</h3>
                                    <table style='width: 100%; border-collapse: collapse;'>
                                        <thead>
                                            <tr>
                                                <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>{L["Product"]}</th>
                                                <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>{L["Price"]}</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {productsHtml}
                                        </tbody>
                                    </table>

                                    <h3>{L["DeliveryDetailsHeading"]}</h3>
                                    <p><strong>{L["Address"]}</strong> {address.Street}, {address.CityName}, {address.CountryName}</p>
                                    <p><strong>{L["ContactPhone"]}</strong> {address.PhoneNumber}</p>

                                    <br>
                                    <p>{L["QuestionsContactUs"]}</p>

                                    <p>{L["Regards"]},<br><strong>{L["HandmadeByDoniTeam"]}</strong></p>
                                </body>
                                </html>
                        ";

            return emailBody;
        }

        public string GetOrderSentEmail(UserOrder userOrder)
        {
            var fullName = $"{userOrder.User.FirstName} {userOrder.User.LastName}";
            var address = userOrder.Address;
            var order = userOrder.Order;
            var orderDate = userOrder.CreaateOn.ToString("dd.MM.yyyy");
            var deliveryCompany = address.DeliveryCompany.Name;
            var paymentMethod = address.MethodPayment.Method;
            var shipmentNote = !string.IsNullOrEmpty(userOrder.ShipmentNoteNumber)
                ? $"<p><strong>{L["ShipmentNoteNumber"]}</strong> {userOrder.ShipmentNoteNumber}</p>"
                : string.Empty;

            var productsHtml = "";
            foreach (var product in order.Products)
            {
                productsHtml += $@"<tr><td style='padding: 8px; border: 1px solid #ccc;'>{product.Title}</td><td style='padding: 8px; border: 1px solid #ccc;'>{product.Price:C}</td></tr>";
            }

            string emailBody = $@"
                    <!DOCTYPE html>
                    <html lang='bg'>
                    <head>
                        <meta charset='UTF-8'>
                        <title>{L["OrderSentEmailTitle"]}</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                        <h2 style='color: #0066cc;'>{L["OrderSentEmailHeading"]}</h2>
                        <p>{L["Greeting"]}, <strong>{fullName}</strong>,</p>
                        <p>{L["OrderSentMessage"]}</p>

                        <h3>{L["OrderDetailsHeading"]}</h3>
                        <p><strong>{L["OrderNumberLabel"]}</strong> {userOrder.OrderId}</p>
                        <p><strong>{L["OrderDateLabel"]}</strong> {orderDate}</p>
                        <p><strong>{L["TotalPriceLabel"]}</strong> {userOrder.TotalPrice:C}</p>
                        <p><strong>{L["Status"]}</strong> {L["OrderSent"]}</p>
                        {shipmentNote}

                        <h3>{L["PurchasedProductsHeading"]}</h3>
                        <table style='width: 100%; border-collapse: collapse;'>
                            <thead>
                                <tr>
                                    <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>{L["Product"]}</th>
                                    <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>{L["Price"]}</th>
                                </tr>
                            </thead>
                            <tbody>
                                {productsHtml}
                            </tbody>
                        </table>

                        <h3>{L["DeliveryDetailsHeading"]}</h3>
                        <p><strong>{L["Address"]}</strong> {address.Street}, {address.CityName}, {address.CountryName}</p>
                        <p><strong>{L["ContactPhone"]}</strong> {address.PhoneNumber}</p>
                        <p><strong>{L["Courier"]}</strong> {deliveryCompany}</p>
                        <p><strong>{L["MethodPayment"]}</strong> {paymentMethod}</p>

                        <br>
                        <p>{L["QuestionsContactUs"]}</p>

                        <p>{L["Regards"]},<br><strong>{L["HandmadeByDoniTeam"]}</strong></p>
                    </body>
                    </html>";

            return emailBody;
        } 

    }
}

