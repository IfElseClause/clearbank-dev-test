# Notes

Write tests to support the below.
1. Create interfaces for AccountDataStore and BackupDataStore (IAccountDataStore) to support DI.
2. Refactor to add an AccountDataStore factory which should instantiate either AccountDataStore or BackupDataStore based on Configuration.
3. Move payment scheme validation into single classes with interface (IPaymentSchemeValidator) - moving responsibilty out of payment service.
4. Refactor to add an PaymentSchemeValidator factory to provide the instantiation of the correct validator for the payment scheme.
5. Simplify logic in payment service using the above.
6. Wrap logic in a transaction to make sure making payments are atomic and rollback as expected.
7. ~~Potential for MakePaymentResult builder to move intakstiation outside of the payment service.~~
8. Convert Data/Models into record types to support immutability.
9. Improve logging

## Additional notes
In the past I've used exceptions for error handling instead of returning a response object with a success flag. Reasoning behind using exceptions is to avoid the calling code ignoring the success flags which could lead to potential bugs.
However, exceptions do come with a performance hit and in some scenarios can be less readable.

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