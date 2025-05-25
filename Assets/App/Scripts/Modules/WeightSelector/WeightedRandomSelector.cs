using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Modules.WeightSelector
{
    public class WeightedRandomSelector
    {
        public T Choose<T>(List<WeightedItem<T>> items)
        {
            float totalWeight = items.Sum(i => i.Weight);
            float roll = Random.Range(0f, totalWeight);
            float cumulative = 0;

            foreach (var item in items)
            {
                cumulative += item.Weight;
                if (roll < cumulative)
                    return item.Item;
            }

            return default;
        }

    }
}