using System;

namespace App.Scripts.Features.GameResources.Providers
{
    public class ResourceProvider : IResourceProvider
    {
        public event Action OnValueChanged;
        
        public int Value { get; protected set; }
        
        public virtual void ChangeValue(int value)
        {
            Value += value;
            Clamp();
            OnValueChanged?.Invoke();
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