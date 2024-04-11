

using HandmadeByDoniApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HandmadeByDoniApp.Data.Configuration
{
    public class MethodPaymentConfiguration:IEntityTypeConfiguration<MethodPayment>
    {
        public void Configure(EntityTypeBuilder<MethodPayment> builder)
        {

            builder.HasData(SeedMethodPayment());
        }
        private ICollection<MethodPayment> SeedMethodPayment()
        {

            MethodPayment cash = new MethodPayment()
            {
                Id = 1,
                Method = "Cash payment on delivery"
            };

            MethodPayment card = new MethodPayment()
            {
                Id = 2,
                Method = "Card payment on delivery"
            };

            ICollection<MethodPayment> methodPayments = new HashSet<MethodPayment>();
            methodPayments.Add(cash);
            methodPayments.Add(card);

            return methodPayments;
        }
    }
}
