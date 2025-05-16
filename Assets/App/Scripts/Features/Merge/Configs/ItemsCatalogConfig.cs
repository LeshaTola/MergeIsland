using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Configs
{
    [CreateAssetMenu(fileName = "MergeItemsCatalogConfig", menuName = "Configs/Merge/Catalog")]
    public class ItemsCatalogConfig : SerializedScriptableObject
    {
        [field: SerializeField] [field: ReadOnly]
        public string Id { get; private set; }

        [field: SerializeField] public List<ItemConfig> ItemsCatalog { get; private set; }
        [field: SerializeField] public List<ItemConfig> EmittersCatalog { get; private set; }

        public bool IsInCatalog(string id)
        {
            return FindItemIndex(id) != -1
                   || FindEmitterIndex(id) != -1;
        }

        public ItemConfig GetNextLevelConfig(string id)
        {
            var mergeItemConfig = GetNextItemConfig(id);
            if (mergeItemConfig != null)
            {
                return mergeItemConfig;
            }

            mergeItemConfig = GetNextEmitterConfig(id);
            return mergeItemConfig;
        }

        private ItemConfig GetNextEmitterConfig(string id)
        {
            var index = FindItemIndex(id);
            if (index != -1)
            {
                var nextIndex = index + 1;
                return nextIndex < EmittersCatalog.Count ? EmittersCatalog[nextIndex] : null;
            }

            return null;
        }

        private ItemConfig GetNextItemConfig(string id)
        {
            var index = FindItemIndex(id);
            if (index != -1)
            {
                var nextIndex = index + 1;
                return nextIndex < ItemsCatalog.Count ? ItemsCatalog[nextIndex] : null;
            }

            return null;
        }

        private int FindItemIndex(string id)
        {
            return ItemsCatalog.FindIndex(x => x.Id.Equals(id));
        }


        private int FindEmitterIndex(string id)
        {
            return EmittersCatalog.FindIndex(x => x.Id.Equals(id));
        }

        private void OnValidate()
        {
            Id = name;
        }
    }
}