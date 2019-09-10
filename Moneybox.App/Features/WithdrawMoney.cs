using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationService _notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var account = _accountRepository.GetAccountById(fromAccountId);

            account.Withdraw(amount);
            if (account.AreFundsLow)
            {
                _notificationService.NotifyFundsLow(account.User.Email);
            }

            _accountRepository.Update(account);
        }
    }
}
