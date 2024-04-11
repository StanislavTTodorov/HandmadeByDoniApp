using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class DeliveryCompanyConfiguration : IEntityTypeConfiguration<DeliveryCompany>
    {
        public void Configure(EntityTypeBuilder<DeliveryCompany> builder)
        {

            builder.HasData(SeedDeliveryCompany());
        }
        private ICollection<DeliveryCompany> SeedDeliveryCompany()
        {

            DeliveryCompany econt = new DeliveryCompany()
            {
                Id = 1,
                Name = "Econt"
            };

            DeliveryCompany speedy = new DeliveryCompany()
            {
                Id = 2,
                Name = "Speedy"
            };          

            ICollection<DeliveryCompany> deliveryCompanies = new HashSet<DeliveryCompany>();
            deliveryCompanies.Add(econt);
            deliveryCompanies.Add(speedy);           

            return deliveryCompanies;
        }
    }
}
