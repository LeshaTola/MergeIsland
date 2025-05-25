using App.Scripts.Features.Merge.Elements.Items.Systems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.Merge.Factory
{
    [CreateAssetMenu(fileName = "ItemConfigsFactoryConfig", menuName = "Configs/Merge/Factories/Item")]
    public class ItemConfigsFactoryConfig : SerializedScriptableObject
    {
        [field: SerializeField] public WebItemSystem WebItemSystem { get; private set; }
    }
}