using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Configs
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/Merge/Item")]
    public class ItemConfig : ScriptableObject
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
}