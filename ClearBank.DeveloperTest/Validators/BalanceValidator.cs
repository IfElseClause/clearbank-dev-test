namespace ClearBank.DeveloperTest.Validators
{
    internal class BalanceValidator : IBalanceValidator
    {
        public bool HasSufficientBalance(decimal balance, decimal amount)
        {
            return balance >= amount;
        }
    }
}
