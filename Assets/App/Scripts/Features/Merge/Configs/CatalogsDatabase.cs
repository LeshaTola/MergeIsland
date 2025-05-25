using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Configs
{
    [CreateAssetMenu(fileName = "CatalogsDatabase", menuName = "Configs/Merge/CatalogsDatabase")]
    public class CatalogsDatabase : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<string, ItemsCatalogConfig> Database { get; private set; }

        public ItemConfig GetItemConfig(string id)
        {
            foreach (var catalogConfig in Database)
            {
                if (!catalogConfig.Value.IsInCatalog(id))
                {
                    continue;
                }

                return catalogConfig.Value.GetItemConfig(id);
            }
            return null;
        }
    }
}