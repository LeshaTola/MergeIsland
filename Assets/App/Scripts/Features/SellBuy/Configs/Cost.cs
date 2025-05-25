using System;
using App.Scripts.Features.GameResources.Configs;
using UnityEngine.Serialization;

namespace App.Scripts.Features.SellBuy.Configs
{
    [Serializable]
    public class Cost
    {
        public bool IsAd;
        public ResourceConfig ResourceConfig;
        public int Count;
    }
}