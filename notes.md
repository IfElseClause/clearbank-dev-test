# Notes

Write tests to support the below.
1. Create interfaces for AccountDataStore and BackupDataStore (IAccountDataStore) to support DI (Helping with testability and readability).
2. Refactor to add an AccountDataStore factory which should instantiate either AccountDataStore or BackupDataStore based on Configuration (Focus on moving the logic into a dependency registration because it's not the concern of the payment service which data store should be used)
3. Move payment scheme validation into single classes with interface (IPaymentSchemeValidator) - moving responsibilty out of payment service.  (Supporting with testability and readability - Moving the logic into smaller units of code).
4. Refactor to add an PaymentSchemeValidator factory to provide the instantiation of the correct validator for the payment scheme.  (it's not the concern of the payment service which payment scheme validator should be selected, seperating concerns out).
5. Simplify logic in payment service using the above.
6. Wrap logic in a transaction to make sure making payments are atomic and rollback as expected.
7. ~~Potential for MakePaymentResult builder to move intakstiation outside of the payment service.~~
8. Convert Data/Models into record types to support immutability.
9. Improve/add logging to help support debugging and problems when they occur.

## Additional notes
In the past I've used exceptions for error handling instead of returning a Result object with a success flag. 
Our reasoning behind using exceptions is to avoid the calling code ignoring the success flags which could lead to potential bugs.
However, exceptions do come with a performance hit and in some scenarios can be less readable. I've left in the Result type as to not change the method signature.
An improvement might be to create a generic `Result<TSuccess,TError>` class, which can be inhereted to improve consistency throughout the code base.
e.g. `Result.Success<TSuccess, TError>(true); or Result.Error<TSuccess, TError>(exception);`

Inline with point 8. I have tried to make a habit out of defining types using records. For example, `record AccountBalance(decimal Amount);`, this would allow the develpoer to use types instead of guessing what number type to use.
In the example above, we could take it further by defining a monetary value record, such as:
```
public enum CurrencyType
{
    GBP,
    USD,
    EUR,
}

public record MoneyAmount(decimal Amount, CurrencyType Type = CurrencyType.GBP);

public record AccountBalance(MoneyAmount Money);
```
For brevity and time constraints, I will leave this out.

On point 6. the transaction is arguably not needed now that Account has been changed to a record and only one action is occuring within the transaction so an exception would be rolledback and the underlying objects not affected.
