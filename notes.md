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
