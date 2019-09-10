using System;

namespace Moneybox.App.Domain
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public const decimal Threshold = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public bool AreFundsLow => Balance < Threshold;
        public bool AreFundsHigh => Balance + Threshold > PayInLimit;

        public void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount))
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }
            Withdrawn -= amount;
            Balance -= amount;
        }

        public void PayIn(decimal amount)
        {
            if (!CanPayIn(amount))
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }
            PaidIn += amount;
            Balance += amount;
        }

        private bool CanWithdraw(decimal amount) => (Balance - amount >= 0m);
        private bool CanPayIn(decimal amount) => (PaidIn + amount <= PayInLimit);

    }
}
