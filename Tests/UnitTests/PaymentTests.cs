using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Exceptions;

namespace Tests.UnitTests
{
    public class PaymentTests
    {
        [Fact]
        public void PaymentNotStartedTest()
        {
            var payment = Payment.CreateBonusForCreatingAccount(new User());
            payment.FinalizeBonusForCreatingAccount();
            Assert.Throws<PaymentNotStartedException>(() => payment.FinalizeBonusForCreatingAccount());
        }

        [Fact]
        public void NotSettingPayeeWhenFinalizingPaymentTest()
        {
            var payment = Payment.CreateBonusForCreatingAccount(null);
            Assert.Throws<PayeeNotSetException>(() => payment.FinalizeBonusForCreatingAccount());
        }
    }
}
