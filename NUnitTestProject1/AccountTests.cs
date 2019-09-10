using System;
using Moneybox.App.Domain;
using NUnit.Framework;
using Moq;

namespace Moneybox.AppTests
{
    public class AccountTests
    {
        private Account _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Account();
            _sut.Balance = 100m;
        }

        [Test]
        public void WithdrawShouldThrowExceptionIfInsufficientFunds()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _sut.Withdraw(200m));
            Assert.AreEqual("Insufficient funds to make transfer", exception.Message);
        }

        [Test]
        public void WithdrawShouldThrowExceptionIfNegativeAmount()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _sut.Withdraw(-1));
            Assert.AreEqual("Cannot withdraw a negative amount", exception.Message);
        }

        [Test]
        public void WithdrawShouldUpdateAccountAmounts()
        {
            _sut.Withdraw(80);
            Assert.AreEqual(80, _sut.Withdrawn);
            Assert.AreEqual(20, _sut.Balance);
        }

        [Test]
        public void PayInShouldThrowExceptionIfMaxLimitReached()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _sut.PayIn(10000m));
            Assert.AreEqual("Account pay in limit reached", exception.Message);
        }

        [Test]
        public void PayInShouldThrowExceptionIfNegativeAmount()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _sut.PayIn(-1));
            Assert.AreEqual("Cannot pay in a negative amount", exception.Message);
        }

        [Test]
        public void PayInShouldUpdateAccountAmounts()
        {
            _sut.PayIn(80);
            Assert.AreEqual(80, _sut.PaidIn);
            Assert.AreEqual(180, _sut.Balance);
        }

    }
}

