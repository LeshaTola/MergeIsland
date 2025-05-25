using System;
using App.Scripts.Features.GameResources.Configs;
using App.Scripts.Features.SellBuy.Services;

namespace App.Scripts.Features.GameResources.Providers
{
    public abstract class ResourceProvider : IResourceProvider
    {
        public event Action<int> OnValueChanged;

        public ResourceProvider(ResourceConfig resourceConfig)
        {
            ResourceConfig = resourceConfig;
        }

        public ResourceConfig ResourceConfig { get; }
        public int Value { get; protected set; }
        
        public virtual void ChangeValue(int value)
        {
            Value += value;
            Clamp();
            OnValueChanged?.Invoke(Value);
        }

        public bool IsEnough(int value)
        {
            return Value >= value;
        }

        private void Clamp()
        {
            if (Value < 0)
                Value = 0;
        }
    }
}