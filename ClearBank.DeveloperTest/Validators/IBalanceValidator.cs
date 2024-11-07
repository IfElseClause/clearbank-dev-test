using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators
{
    public interface IBalanceValidator
    {
        bool HasSufficientBalance(Account account, decimal amount);
    }
}
