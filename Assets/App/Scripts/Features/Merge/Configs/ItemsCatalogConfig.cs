using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Configs
{
    public class MergeItemConfig : ScriptableObject
    {
        [field: SerializeField] [field: ReadOnly]
        public string Id { get; private set; }

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        // OnClick Action        

        private void OnValidate()
        {
            Id = name;
        }
    }

    public class MergeItemsCatalogConfig : SerializedScriptableObject
    {
        [field: SerializeField] [field: ReadOnly]
        public string Id { get; private set; }

        [field: SerializeField] public List<MergeItemConfig> ItemsCatalog { get; private set; }
        [field: SerializeField] public List<MergeItemConfig> EmittersCatalog { get; private set; }

        public bool IsInCatalog(string id)
        {
            return FindItemIndex(id) != -1
                   || FindEmitterIndex(id) != -1;
        }

        public MergeItemConfig GetNextLevelConfig(string id)
        {
            var mergeItemConfig = GetNextItemConfig(id);
            if (mergeItemConfig != null)
            {
                return mergeItemConfig;
            }

            mergeItemConfig = GetNextEmitterConfig(id);
            return mergeItemConfig;
        }

        private MergeItemConfig GetNextEmitterConfig(string id)
        {
            var index = FindItemIndex(id);
            if (index != -1)
            {
                var nextIndex = index + 1;
                return nextIndex < EmittersCatalog.Count ? EmittersCatalog[nextIndex] : null;
            }

            return null;
        }

        private MergeItemConfig GetNextItemConfig(string id)
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

    public class CatalogsDatabase : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<string, MergeItemsCatalogConfig> Database { get; private set; }
    }
}