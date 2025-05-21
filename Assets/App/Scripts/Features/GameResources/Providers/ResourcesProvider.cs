using App.Scripts.Features.GameResources.Coins.Providers;
using App.Scripts.Features.GameResources.Energy.Providers;
using App.Scripts.Features.GameResources.Gems.Providers;

namespace App.Scripts.Features.GameResources.Providers
{
    public class ResourcesProvider
    {
        public CoinsResourceProvider Coins { get; }
        public GemsResourceProvider Gems { get; }
        public EnergyProvider Stars { get; }

        public ResourcesProvider(CoinsResourceProvider coins, GemsResourceProvider gems, EnergyProvider stars)
        {
            Coins = coins;
            Gems = gems;
            Stars = stars;
        }

    }
}