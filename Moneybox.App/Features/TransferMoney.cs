using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationService _notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = _accountRepository.GetAccountById(fromAccountId);
            var to = _accountRepository.GetAccountById(toAccountId);

            from.Withdraw(amount);
            if (from.AreFundsLow)
            {
                _notificationService.NotifyFundsLow(from.User.Email);
            }

            to.PayIn(amount);
            if (from.AreFundsHigh)
            {
                _notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }

            _accountRepository.Update(from);
            _accountRepository.Update(to);
        }
    }
}
