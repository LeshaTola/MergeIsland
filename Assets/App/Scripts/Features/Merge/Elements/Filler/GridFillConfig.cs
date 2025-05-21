using System;
using System.Collections.Generic;
using App.Scripts.Features.Merge.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Filler
{
    [CreateAssetMenu(fileName = "GridFillConfig", menuName = "Configs/Grid/Filler")]
    public class GridFillConfig : SerializedScriptableObject
    {
        [field: SerializeField] public List<FillValue> Filling { get; private set; }
    }

    [Serializable]
    public class FillValue
    {
        public Vector2Int Position;
        public ItemConfig ItemConfig;
        public bool IsInWeb;
    }
}