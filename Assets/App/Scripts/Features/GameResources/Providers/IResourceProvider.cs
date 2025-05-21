using System;

namespace App.Scripts.Features.GameResources.Providers
{
    public interface IResourceProvider
    {
        event Action OnValueChanged;
        int Value { get; }
        void ChangeValue(int value);
        bool IsEnough(int value);
    }
}