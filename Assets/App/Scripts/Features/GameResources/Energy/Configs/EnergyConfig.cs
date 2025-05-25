using App.Scripts.Features.GameResources.Configs;
using App.Scripts.Features.SellBuy.Services;
using UnityEngine;

namespace App.Scripts.Features.GameResources.Energy.Configs
{
    [CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/Resources/Energy")]
    public class EnergyConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxEnergy { get; private set; }
        [field: SerializeField] public float RecoveryTime { get; private set; }
        [field: SerializeField] public int RecoveryValue { get; private set; }
        [field: SerializeField] public ResourceConfig ResourceConfig { get; private set; }
    }
}