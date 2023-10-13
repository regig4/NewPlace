using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Domain.Exceptions;
using Xunit;

namespace Tests.UnitTests
{
    public class PaymentTests
    {
        [Fact]
        public void PaymentNotStartedTest()
        {
            Payment payment = Payment.CreateBonusForCreatingAccount(new User());
            payment.CompleteBonusForCreatingAccount();
            Assert.Throws<PaymentNotStartedException>(() => payment.CompleteBonusForCreatingAccount());
        }

        [Fact]
        public void NotSettingPayeeWhenFinalizingPaymentTest()
        {
            Payment payment = Payment.CreateBonusForCreatingAccount(null);
            Assert.Throws<PayeeNotSetException>(() => payment.CompleteBonusForCreatingAccount());
        }
    }
}
