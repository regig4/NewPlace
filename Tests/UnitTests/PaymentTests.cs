using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PaymentService.ApplicationCore.Domain.Entities;
using PaymentService.Domain.Exceptions;

namespace Tests.UnitTests
{
    public class PaymentTests
    {
        [Fact]
        public void PaymentNotStartedTest()
        {
            var payment = Payment.CreateBonusForCreatingAccount(new User());
            payment.CompleteBonusForCreatingAccount();
            Assert.Throws<PaymentNotStartedException>(() => payment.CompleteBonusForCreatingAccount());
        }

        [Fact]
        public void NotSettingPayeeWhenFinalizingPaymentTest()
        {
            var payment = Payment.CreateBonusForCreatingAccount(null);
            Assert.Throws<PayeeNotSetException>(() => payment.CompleteBonusForCreatingAccount());
        }
    }
}
