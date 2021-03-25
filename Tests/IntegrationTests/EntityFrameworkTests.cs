using PaymentService.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.IntegrationTests
{
    public class EntityFrameworkTests
    {
        [Fact] 
        public void AddNewRecordContainingExistingFK()
        {
            ContextFactory factory = new ContextFactory();

            var db = factory.Create();

            //db.State
        }
    }
}
