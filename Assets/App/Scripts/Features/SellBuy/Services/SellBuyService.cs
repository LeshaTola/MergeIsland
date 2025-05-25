using App.Scripts.Features.Advertisement.Providers;
using App.Scripts.Features.GameResources.Providers;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.SellBuy.Configs;
using UnityEngine;

namespace App.Scripts.Features.SellBuy.Services
{
    public class SellBuyService
    {
        private readonly AdvertisementProvider _advertisementProvider;
        private readonly ResourcesProvider _resourcesProvider;
        private readonly SellBuyConfig _config;

        public SellBuyService(AdvertisementProvider advertisementProvider,
            ResourcesProvider resourcesProvider, SellBuyConfig config)
        {
            _advertisementProvider = advertisementProvider;
            _resourcesProvider = resourcesProvider;
            _config = config;
        }

        public void Sell(Cost cost)
        {
            _resourcesProvider.Change(cost.ResourceConfig, cost.Count);
        }

        public bool TryBuy(Cost cost)
        {
            return _resourcesProvider.TrySpend(cost.ResourceConfig, cost.Count);
        }
        
        public Cost GetSellCost(ItemConfig item)
        {
            return new Cost
            {
                ResourceConfig = _config.SellResourceConfig,
                Count = Mathf.RoundToInt((item.Level + 1) * _config.SellMultiplier),
            };
        }

        public Cost GetBuyCostWithAd(ItemConfig item)
        {
            if (item.Level < _config.MaxLevelForReward && _advertisementProvider.IsReady)
            {
                return new() {IsAd = true};
            }
            return GetBuyCost(item);
        }

        public Cost GetBuyCost(ItemConfig item)
        {
            return new Cost
            {
                ResourceConfig = _config.BuyResourceConfig,
                Count = Mathf.RoundToInt((item.Level + 1) * _config.BuyMultiplier),
            };
        }
    }
}