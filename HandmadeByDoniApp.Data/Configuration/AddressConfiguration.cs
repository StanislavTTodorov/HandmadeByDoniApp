using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
           // builder.HasData(SeedAddresses());
        }

        private ICollection<Address> SeedAddresses()
        {
            ICollection<Address> addresses = new HashSet<Address>();
           Address address = new Address()
           {
               Id = Guid.Parse("E2134209-BFE1-4AD3-8B89-E3C8F95B55C0"),
               CountryName = "Bulgaria",
               CityName = "Varna",
               Street = "137 Slivnitsa Blvd",
               ClientId = Guid.Parse("C5AE3631-31A1-4369-9F2E-8EEC685C98EB"),
               DeliveryCompanyId = 1,
               MethodPaymentId = 1,
               PhoneNumber = "0898554383"

           };
            addresses.Add(address);
            return addresses;
        }
    }
}
