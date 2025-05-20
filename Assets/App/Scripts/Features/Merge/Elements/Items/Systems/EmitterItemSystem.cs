using App.Scripts.Features.Energy.Providers;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public class EmitterItemSystem : ItemSystem
    {
        [SerializeField] private ItemsCatalogConfig _itemsCatalogConfig;

        private readonly ItemFactory _itemFactory;
        private readonly ItemConfigsFactory _itemConfigsFactory;
        private readonly EnergyProvider _energyProvider;
        private readonly Grid _grid;

        public EmitterItemSystem(ItemFactory itemFactory, 
            ItemConfigsFactory itemConfigsFactory,
            EnergyProvider energyProvider,
            Grid grid)
        {
            _itemConfigsFactory = itemConfigsFactory;
            _energyProvider = energyProvider;
            _itemFactory = itemFactory;
            _grid = grid;
        }

        public override void Start()
        {
            Item.Visual.EmitterActiveSetActive(true);
        }
        
        public override void Stop()
        {
            Item.Visual.EmitterActiveSetActive(false);
            Item.Visual.EmitterReloadSetActive(false);
        }
        
        public override void Execute()
        {
            SpawnItem();
        }

        public override void Import(ItemSystem original)
        {
            base.Import(original);
            var system = (EmitterItemSystem) original;
            _itemsCatalogConfig = system._itemsCatalogConfig;
        }

        private void SpawnItem()
        {
            if (_energyProvider.CurrentEnergy <= 0)
            {
                return;
            }
            
            _energyProvider.ChangeEnergy(-1);
            var item = GetReadyItem();
            DropItemInSlot(item);
        }

        private Item GetReadyItem()
        {
            var item = _itemFactory.GetItem();
            
            var newConfig = _itemConfigsFactory.GetConfig(_itemsCatalogConfig.ItemsCatalog[0]);
            item.Setup(newConfig);
            
            item.transform.position = Item.transform.position;
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