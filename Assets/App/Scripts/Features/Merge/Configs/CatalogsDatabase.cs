using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Configs
{
    [CreateAssetMenu(fileName = "CatalogsDatabase", menuName = "Configs/Merge/CatalogsDatabase")]
    public class CatalogsDatabase : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<string, ItemsCatalogConfig> Database { get; private set; }
    }
}