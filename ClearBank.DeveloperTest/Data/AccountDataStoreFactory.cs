using ClearBank.DeveloperTest.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace ClearBank.DeveloperTest.Data
{
    internal class AccountDataStoreFactory : IAccountDataStoreFactory
    {
        private const string BackupTypeName = "Backup";
        private readonly IOptions<DataStoreOptions> _options;
        private readonly IServiceProvider _provider;

        public AccountDataStoreFactory(
            IOptions<DataStoreOptions> options,
            IServiceProvider provider)
        {
            _options = options;
            _provider = provider;
        }

        public IAccountDataStore Create()
        {
            return _options.Value.Type == BackupTypeName
                ? _provider.GetRequiredService<BackupAccountDataStore>()
                : _provider.GetRequiredService<AccountDataStore>();
        }
    }
}
