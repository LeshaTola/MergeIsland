using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Grid = App.Scripts.Features.Merge.Elements.Grid;

namespace App.Scripts.Scenes.Gameplay.Features
{
    public class SpawnItemButton:MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ItemsCatalogConfig itemsCatalogConfig;
        
        private IItemFactory _itemFactory;
        private Grid _grid;
        

        [Inject]
        [SerializeField] private void Construct(Grid grid, IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _grid = grid;
        }

        private void Awake()
        {
            _button.onClick.AddListener(SpawnItem);
        }

        private void SpawnItem()
        {
            var item = _itemFactory.GetItem();
            item.Setup(itemsCatalogConfig.ItemsCatalog[0]);
            
            var slot = _grid.GetUnusedSlot();
            slot.DropItem(item);
            item.MoveToParent().Forget();
        }
    }
}