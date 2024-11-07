using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    internal class BalanceValidator
    {
        public bool HasSufficientBalance(Account account, decimal amount)
        {
            return account.Balance >= amount;
        }
    }
}
