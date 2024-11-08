namespace ClearBank.DeveloperTest.Validators
{
    public interface IBalanceValidator
    {
        bool HasSufficientBalance(decimal balance, decimal amount);
    }
}
