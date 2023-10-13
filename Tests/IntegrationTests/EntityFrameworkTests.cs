using PaymentService.Infrastructure.Factories;
using Xunit;

namespace Tests.IntegrationTests
{
    public class EntityFrameworkTests
    {
        [Fact]
        public void AddNewRecordContainingExistingFK()
        {
            ContextFactory factory = new ContextFactory();

            Infrastructure.PaymentDb db = factory.Create();

            //db.State
        }
    }
}
