using UnityEngine;

namespace App.Scripts.Features.Advertisement.Providers
{
    [CreateAssetMenu(fileName = "AdvertisementConfig", menuName = "Configs/Advertisement")]
    public class AdvertisementConfig : ScriptableObject
    {
        [field: SerializeField] public float InterstitialCooldown { get; private set; } = 300f;
    }
}