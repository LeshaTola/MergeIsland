using App.Scripts.Features.Merge.Elements.Items.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Configs
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/Merge/Item")]
    public class ItemConfig : SerializedScriptableObject
    {
        [field: SerializeField] [field: ReadOnly]
        public string Id { get; private set; }

        [field: SerializeField] [field: ReadOnly]
        public int Level { get; set; }

        [field: SerializeField] [field: ReadOnly]
        public bool IsLastLevel { get; set; }

        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] [field: PreviewField(Height = 100)]
        public Sprite Sprite { get; private set; }

        [field: SerializeField] public int Cost { get; private set; }

        [field: Header("Systems")]
        [field: SerializeField] public ItemSystem System { get; private set; }

        public void Initialize(ItemSystem system)
        {
            System = system;
        }

        private void OnValidate()
        {
            Id = name;
        }
    }
}