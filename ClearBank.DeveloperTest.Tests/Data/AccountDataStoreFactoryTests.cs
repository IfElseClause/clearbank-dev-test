using AutoFixture.Xunit2;
using AutoFixture;
using ClearBank.DeveloperTest.Data;
using System;
using Microsoft.Extensions.Options;
using ClearBank.DeveloperTest.Options;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    public class AccountDataStoreFactoryTests
    {
        internal void Create_DataStoreTypeIsDefault_ReturnsAccountDataStore(
            [Frozen] IServiceProvider serviceProvider,
            AccountDataStoreFactory sut,
            IFixture fixture)
        {
            // Arrange
            IOptions<DataStoreOptions> options = Microsoft.Extensions.Options.Options
                .Create(new DataStoreOptions()
                {
                    Type = "Default"
                });

            fixture.Inject(options);

            // Act
            var result = sut.Create();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AccountDataStore>();
        }

        internal void Create_DataStoreTypeIsBackup_ReturnsBackupAccountDataStore(
            [Frozen] IServiceProvider serviceProvider,
            AccountDataStoreFactory sut,
            IFixture fixture)
        {
            // Arrange
            IOptions<DataStoreOptions> options = Microsoft.Extensions.Options.Options
                .Create(new DataStoreOptions()
                {
                    Type = "Backup"
                });

            fixture.Inject(options);

            // Act
            var result = sut.Create();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BackupAccountDataStore>();
        }
    }
}
