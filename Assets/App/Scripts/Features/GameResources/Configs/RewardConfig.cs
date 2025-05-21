using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.GameResources.Configs
{
    [CreateAssetMenu(fileName = "RewardConfig", menuName = "Configs/Rewards/Reward")]
    public class RewardConfig : ScriptableObject
    {
        [field: SerializeField] [field: ReadOnly]
        public string Id { get; private set; }

        // [field: SerializeField] public ItemConfig Reward { get; private set; }
        [field: SerializeField] public Color Color { get; private set; } = Color.white;
        [field: SerializeField] public int Count { get; set; }

        private void OnValidate()
        {
            Id = name;
        }
    }
}