using HandmadeByDoniApp.Data.Models;
using HandmadeByDoniApp.Services.Data.DataRepository;
using HandmadeByDoniApp.Services.Data.Interfaces;
using HandmadeByDoniApp.Web.ViewModels.Address;
using HandmadeByDoniApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace HandmadeByDoniApp.Services.Data.Service
{
    public class AddressService : IAddressService
    {
        private readonly IRepository repository;

        public AddressService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<DeliveryCompanyFormModel>> AllDeliveryCompaniesAsync()
        {
            IEnumerable<DeliveryCompanyFormModel> deliveryCompanies =
                await this.repository
                 .AllReadOnly<DeliveryCompany>()
                 .Select(c => new DeliveryCompanyFormModel()
                 {
                     Id = c.Id,
                     Name = c.Name
                 })
                 .ToArrayAsync();

            return deliveryCompanies;

        }

        public async Task<IEnumerable<MethodPaymentFormModel>> AllMethodPaymentsAsync()
        {
            IEnumerable<MethodPaymentFormModel> methodPayments =
                await this.repository
                 .AllReadOnly<MethodPayment>()
                 .Select(c => new MethodPaymentFormModel()
                 {
                     Id = c.Id,
                     Method = c.Method
                 })
                 .ToArrayAsync();

            return methodPayments;
        }


        public async Task CreateAddressAsync(AddressFormModel formModel, string userId)
        {
            Address newAddress = new Address()
            {
                CityName = formModel.CityName,
                CountryName = formModel.CountryName,
                Street = formModel.Street,
                PhoneNumber = formModel.PhoneNumber,
                MethodPaymentId = formModel.MethodPaymentId,
                DeliveryCompanyId = formModel.DeliveryCompanyId,
                ClientId = Guid.Parse(userId)
            };

            await repository.AddRangeAsync(newAddress);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> DeliveryCompanyExistsByIdAsync(int deliveryCompanyId)
        {
            bool exists = await this.repository
                .All<DeliveryCompany>()
                .AnyAsync(g => g.Id == deliveryCompanyId);

            return exists;
        }

        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            bool exist = await this.repository
                .AllReadOnly<Address>()
                .AnyAsync(a => a.ClientId.ToString() == userId);
               
            return exist;
        }

        public async Task<AddressFormModel?> GetAddressByUserIdAsync(string userId)
        {
            Address? address = await this.repository
                .AllReadOnly<Address>()
                .Include(a=>a.DeliveryCompany)
                .Include(a => a.MethodPayment)
                .FirstOrDefaultAsync(a=>a.ClientId.ToString()==userId);
            if (address == null)
            {
                return null;
            }

            return new AddressFormModel()
            {
                CountryName = address.CountryName,
                CityName = address.CityName,
                Street = address.Street,
                PhoneNumber = address.PhoneNumber,
                DeliveryCompanyId = address.DeliveryCompany.Id,                
                MethodPaymentId  = address.MethodPayment.Id
            };
        }

        public async Task<string> GetDeliveryCompanyNameAsyng(int id)
        {
            string[] name = await this.repository
                .AllReadOnly<DeliveryCompany>()
                .Where(d=>d.Id==id)
                .Select(d=>d.Name)
                .ToArrayAsync();
            return name[0];
        }

        public async Task<string> GetMethodPaymentNameAsyng(int id)
        {
            string[] name = await this.repository
             .AllReadOnly<MethodPayment>()
             .Where(d => d.Id == id)
             .Select(d => d.Method)
             .ToArrayAsync();
            return name[0];
        }

            public async Task<bool> MethodPaymentExistsByIdAsync(int methodPaymentId)
        {
            bool exists = await this.repository
                 .All<MethodPayment>()
                 .AnyAsync(g => g.Id == methodPaymentId);

            return exists;
        }
    }
}
