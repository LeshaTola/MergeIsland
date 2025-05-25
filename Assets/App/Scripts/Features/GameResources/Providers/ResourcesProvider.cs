using System;
using System.Collections.Generic;
using App.Scripts.Features.GameResources.Configs;
using App.Scripts.Features.SellBuy.Services;
using UnityEngine;

namespace App.Scripts.Features.GameResources.Providers
{
    public class ResourcesProvider
    {
        public event Action<string, int> OnResourceChanged;
        
        private readonly Dictionary<string, IResourceProvider> _resources = new();
        
        public ResourcesProvider(List<IResourceProvider> logics)
        {
            foreach (var logic in logics)
            {
                _resources[logic.ResourceConfig.Id] = logic;
                logic.OnValueChanged += value => OnResourceChanged?.Invoke(logic.ResourceConfig.Id, value);
            }
        }

        public int Get(ResourceConfig resourceConfig) =>
            _resources.TryGetValue(resourceConfig.Id, out var r) ? r.Value : 0;

        public void Change(ResourceConfig resourceConfig, int amount) =>
            _resources[resourceConfig.Id].ChangeValue(amount);

        public bool IsEnough(ResourceConfig resourceConfig, int amount) =>
            _resources[resourceConfig.Id].IsEnough(amount);

        public bool TrySpend(ResourceConfig resourceConfig, int amount)
        {
            if (!IsEnough(resourceConfig, amount))
            {
                return false;
            }

            Change(resourceConfig, -amount);
            return true;

        }
    }
}