using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    [TestFixture]
    public class BankTest
    {
        Bank bank;

        [SetUp]
        public void SetUp()
        {
            bank = new Bank();
            bank.CreateAccount("1234");
        }


        [Test]
        public void StartingBalanceIsZero()
        {
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }


        [Test]
        public void DepositOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }


        [Test]
        public void DepositMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Deposit("1234", 15000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(25000));
        }


        [Test]
        public void DepositNegativeAmount()
        {
            Assert.That(() => bank.Deposit("1234", -10000), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void DepositMultipleAccounts()
        {
            bank.CreateAccount("5678");
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }


        [Test]
        public void GetBalanceOfInvalidAccount()
        {
            Assert.That(() => bank.GetBalance("9999"), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void DepositToInvalidAccount()
        {
            Assert.That(() => bank.Deposit("9999", 5000), Throws.TypeOf<ArgumentException>());
        }


        [Test]

        public void WithdrawSuccessful()
        {
            bank.Deposit("1234", 10000);
            bool result = bank.Withdraw("1234", 5000);

            Assert.That(result, Is.True);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(5000));
        }


        [Test]
        public void WithdrawNegativeAmount()
        {
            Assert.That(() => bank.Withdraw("1234", -1000), Throws.TypeOf<ArgumentException>());
        }


        [Test]

        public void WithdrawFromInvalidAccount()
        {
            Assert.That(() => bank.Withdraw("9999", 1000), Throws.TypeOf<ArgumentException>());
        }


        [Test]

        public void WithdrawMoreThanBalance()
        {

            bank.Deposit("1234", 10000);
            bool result = bank.Withdraw("1234", 15000);

            Assert.That(result, Is.False);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }


        [Test]

        public void TransferFromInvalidAccount()
        {
            Assert.That(() => bank.Transfer("1234", "9999", 5000), Throws.TypeOf<ArgumentException>());
        }


        [Test]
        public void TransferToInvalidAccount()
        {
            Assert.That(() => bank.Transfer("9999", "1234", 5000), Throws.TypeOf<ArgumentException>());
        }


        [Test]

        public void TransferAmountZeroOrNegative()
        {
            Assert.That(() => bank.Transfer("1234", "5678", 0), Throws.TypeOf<ArgumentException>());
        }


        [Test]

        public void TransferMoreThanBalance()
        {
            bank.Deposit("1234", 10000);
            bank.CreateAccount("5678");
            bool result = bank.Transfer("1234", "5678", 15000);

            Assert.That(result, Is.False);

            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }


        [Test]

        public void TransferSuccessful()
        {
            bank.Deposit("1234", 10000);
            bank.CreateAccount("5678");
            bool result = bank.Transfer("1234", "5678", 5000);

            Assert.That(result, Is.True);

            Assert.That(bank.GetBalance("1234"), Is.EqualTo(5000));
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(5000));
        }


        [Test]
        public void TransferToSameAccount()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.Transfer("1234", "1234", 5000), Throws.TypeOf<ArgumentException>());
        }


        [Test]

        public void ClosedAccountNoGetBalance()
        {
            bank.CloseAccount("1234");
            Assert.That(() => bank.GetBalance("1234"), Throws.TypeOf<ArgumentException>());
        }



        [Test]

        public void CloseAccountWithMoneyOn()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.CloseAccount("1234"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]

        public void CloseAccount()
        {
            bank.CloseAccount("1234");
            Assert.That(() => bank.CloseAccount("1234"), Throws.TypeOf<ArgumentException>());
        }


    }
}
