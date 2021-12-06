using Domain.Core.ExternalProviderContract;
using Infrastructure.Card2CardProvider.AyandehBank;
using Infrastructure.Card2CardProvider.AyandehBank.Model;
using Infrastructure.Card2CardProvider.MellatBank;
using Infrastructure.Card2CardProvider.MellatBank.Model;
using Infrastructure.Card2CardProvider.SamanBank;
using Infrastructure.Card2CardProvider.SamanBank.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Card2CardApi.Service
{
    public static class DependancyInjection
    {
        public static void RegisterProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICard2CardProvider, SamanBankCard2CardProvider>();
            services.AddScoped<ICard2CardProvider, MellatBankCard2CardProvider>();
            services.AddScoped<ICard2CardProvider, AyandehBankCard2CardProvider>();
            services.AddScoped<ICardToCardProviderResolver, CardToCardProviderResolver>();

            services.Configure<SamanServiceConfig>(configuration.GetSection("SamanBankProvider"));
            services.Configure<MellatServiceConfig>(configuration.GetSection("MellatBankProvider"));
            services.Configure<AyandehServiceConfig>(configuration.GetSection("AyandehBankProvider"));

        }
    }
}
