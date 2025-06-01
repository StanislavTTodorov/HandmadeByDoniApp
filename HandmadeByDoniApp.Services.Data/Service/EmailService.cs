using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.User;
using MailKit.Net.Smtp; 
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class EmailService:IEmailService
    {
        private readonly IRepository repository;
        private ILogger<EmailService> logger;


        public EmailService(IRepository repository, ILogger<EmailService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Stanislav", "stanislavttodorov7@gmail.com"));
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Subject = subject;

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
                await client.AuthenticateAsync("stanislavttodorov7@gmail.com", "lhfz rjfd lljw ctoi");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

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
                ? $"<p><strong>Номер на товарителница:</strong> {userOrder.ShipmentNoteNumber}</p>"
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
                        <title>Потвърждение на поръчка</title>
                    </head>
                    <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                        <h2 style='color: #0066cc;'>Потвърждение на Вашата поръчка</h2>
                        <p>Здравейте, <strong>{fullName}</strong>,</p>

                        <p>Благодарим Ви, че направихте поръчка от нашия онлайн магазин!</p>

                        <h3>📦 Детайли на поръчката</h3>
                        <p><strong>Номер на поръчка:</strong> {userOrder.OrderId}</p>
                        <p><strong>Дата на поръчка:</strong> {orderDate}</p>
                        <p><strong>Обща сума:</strong> {userOrder.TotalPrice:C}</p>
                        <p><strong>Статус:</strong> {(userOrder.IsSent ? "Изпратена" : "В процес на подготовка")}</p>
                        {shipmentNote}

                        <h3>🛒 Закупени продукти</h3>
                        <table style='width: 100%; border-collapse: collapse;'>
                            <thead>
                                <tr>
                                    <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>Продукт</th>
                                    <th style='padding: 8px; border: 1px solid #ccc; background-color: #f2f2f2;'>Цена</th>
                                </tr>
                            </thead>
                            <tbody>
                                {productsHtml}
                            </tbody>
                        </table>

                        <h3>🚚 Данни за доставка</h3>
                        <p><strong>Адрес:</strong> {address.Street}, {address.CityName}, {address.CountryName}</p>
                        <p><strong>Телефон за контакт:</strong> {address.PhoneNumber}</p>
                        <p><strong>Куриер:</strong> {deliveryCompany}</p>
                        <p><strong>Метод на плащане:</strong> {paymentMethod}</p>

                        <br>
                        <p>Ако имате въпроси, не се колебайте да се свържете с нас!</p>

                        <p>С уважение,<br><strong>Екипът на HandmadeByDoni</strong></p>
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
                                    <title>Потвърждение на имейл</title>
                                </head>
                                <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
                                    <h2 style='color: #0066cc;'>Добре дошли, {user.FirstName}!</h2>
                                    <p>Благодарим Ви, че се регистрирахте в нашата платформа.</p>

                                    <p>Моля, потвърдете Вашия имейл адрес, като кликнете на бутона по-долу:</p>

                                    <p style='margin: 30px 0;'>
                                        <a href='{confirmationLink}' style='padding: 10px 20px; background-color: #28a745; color: #fff;
                                        text-decoration: none; border-radius: 5px;'>Потвърди имейл</a>
                                    </p>

                                    <p>Ако не сте се регистрирали при нас, игнорирайте този имейл.</p>

                                    <p>С уважение,<br><strong>Екипът на HandmadeByDoni</strong></p>
                                </body>
                                </html>
                                ";

            return emailBody;
        }



    }
}
