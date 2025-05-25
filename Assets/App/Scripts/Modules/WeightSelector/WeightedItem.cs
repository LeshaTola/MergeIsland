using System;

namespace App.Scripts.Modules.WeightSelector
{
    [Serializable]
    public class WeightedItem<T>
    {
        public T Item;
        public float Weight;

        public WeightedItem(T item, float weight)
        {
            Item = item;
            Weight = weight;
        }
    }
}