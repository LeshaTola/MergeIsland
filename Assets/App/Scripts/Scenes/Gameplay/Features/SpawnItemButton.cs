using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;
using Grid = App.Scripts.Features.Merge.Elements.Grid;

namespace App.Scripts.Scenes.Gameplay.Features
{
    public class SpawnItemButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [FormerlySerializedAs("itemsCatalogConfig")] [SerializeField] 
        private ItemsCatalogConfig _itemsCatalogConfig;
        
        private ItemFactory _itemFactory;
        private ItemConfigsFactory _itemConfigsFactory;
        private Grid _grid;
        

        [Inject]
        private void Construct(Grid grid, ItemFactory itemFactory,ItemConfigsFactory itemConfigsFactory)
        {
            _itemConfigsFactory = itemConfigsFactory;
            _itemFactory = itemFactory;
            _grid = grid;
        }

        private void Start()
        {
            _button.onClick.AddListener(SpawnItem);
        }

        private void SpawnItem()
        {
            var item = GetReadyItem();
            DropItemInSlot(item);
        }

        private Item GetReadyItem()
        {
            var item = _itemFactory.GetItem();
            var newConfig = _itemConfigsFactory.GetConfig(_itemsCatalogConfig.ItemsCatalog[0]);
            item.Setup(newConfig);
            return item;
        }

        private void DropItemInSlot(Item item)
        {
            var slot = _grid.GetUnusedSlot();
            slot.DropItem(item);
            item.MoveToParent().Forget();
        }
    }
}