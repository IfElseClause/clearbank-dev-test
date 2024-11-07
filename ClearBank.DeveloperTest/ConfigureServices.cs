using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Options;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Validators;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection ServiceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions()
            .Configure<DataStoreOptions>(configuration.GetSection("DataStoreOptions"))

            .AddTransient<IPaymentService, PaymentService>()
            .AddSingleton<IAccountDataStoreFactory, AccountDataStoreFactory>()
            .AddTransient(provider =>
                provider.GetRequiredService<IAccountDataStoreFactory>().Create())
            .AddTransient<AccountDataStore>()
            .AddTransient<BackupAccountDataStore>()

            .AddScoped<BacsPaymentSchemeValidator>()
            .AddScoped<ChapsPaymentSchemeValidator>()
            .AddScoped<FasterPaymentSchemeValidator>()

            .AddSingleton<IPaymentSchemeValidatorFactory, PaymentSchemeValidatorFactory>();

        return services;
    }
}