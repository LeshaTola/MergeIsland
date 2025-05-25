using System;
using App.Scripts.Features.GameResources.Configs;

namespace App.Scripts.Features.GameResources.Providers
{
    public interface IResourceProvider
    {
        event Action<int> OnValueChanged;
        int Value { get; }
        ResourceConfig ResourceConfig { get; }
        void ChangeValue(int value);
        bool IsEnough(int value);
    }
}