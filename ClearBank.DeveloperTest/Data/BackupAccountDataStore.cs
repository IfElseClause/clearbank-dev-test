﻿using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public class BackupAccountDataStore : IAccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access backup data base to retrieve account, code removed for brevity 
            return new Account(accountNumber, 0, AccountStatus.Live, AllowedPaymentSchemes.Bacs);
        }

        public void UpdateAccount(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
