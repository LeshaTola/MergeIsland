using App.Scripts.Features.GameResources.Configs;
using UnityEngine;

namespace App.Scripts.Features.SellBuy.Configs
{
    [CreateAssetMenu(fileName = "SellBuyConfig", menuName = "Configs/SellBuy")]
    public class SellBuyConfig: ScriptableObject
    {
        [field: SerializeField] public float SellMultiplier { get; private set; } = 1f;
        [field: SerializeField] public ResourceConfig SellResourceConfig { get; private set; }
        [field: SerializeField] public float BuyMultiplier { get; private set; } = 2f;
        [field: SerializeField] public ResourceConfig BuyResourceConfig { get; private set; }
        [field: SerializeField] public int MaxLevelForReward { get; private set; } = 3;
    }
}