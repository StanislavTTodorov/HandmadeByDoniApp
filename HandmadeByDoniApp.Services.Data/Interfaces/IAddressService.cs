

using HandmadeByDoniApp.Web.ViewModels.Address;

namespace HandmadeByDoniApp.Services.Data.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<DeliveryCompanyFormModel>> AllDeliveryCompaniesAsync();
        Task<IEnumerable<MethodPaymentFormModel>> AllMethodPaymentsAsync();
        Task CreateAddressAsync(AddressFormModel formModel, string userId);
        Task<bool> DeliveryCompanyExistsByIdAsync(int deliveryCompanyId);
        Task<bool> ExistsByUserIdAsync(string userId);
        Task<AddressFormModel?> GetAddressByUserIdAsync(string userId);
        Task<bool> MethodPaymentExistsByIdAsync(int methodPaymentId);
        Task<string> GetDeliveryCompanyNameAsyng(int id);
        Task<string> GetMethodPaymentNameAsyng(int id);
        Task EditAddressAsync(AddressFormModel formModel, string userId);
    }
}
