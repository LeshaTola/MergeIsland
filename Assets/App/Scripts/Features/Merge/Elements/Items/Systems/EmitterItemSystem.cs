using System.Collections.Generic;
using App.Scripts.Features.GameResources.Energy.Providers;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Factory;
using App.Scripts.Features.Merge.Screens;
using App.Scripts.Modules.PopupAndViews;
using App.Scripts.Modules.WeightSelector;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public class EmitterItemSystem : ItemSystem
    {
        [SerializeField] private List<WeightedItem<ItemConfig>> _items;

        private readonly ItemFactory _itemFactory;
        private readonly WeightedRandomSelector _weightedRandomSelector;
        private readonly ItemConfigsFactory _itemConfigsFactory;
        private readonly EnergyProvider _energyProvider;
        private readonly Grid _grid;

        public EmitterItemSystem(WeightedRandomSelector weightedRandomSelector,
            ItemFactory itemFactory,
            ItemConfigsFactory itemConfigsFactory,
            EnergyProvider energyProvider,
            Grid grid)
        {
            _weightedRandomSelector = weightedRandomSelector;
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
            _items = system._items;
        }

        private void SpawnItem()
        {
            if (_energyProvider.Value <= 0)
            {
                return;
            }

            var slot = _grid.GetNearestUnusedSlot(Item.CurrentSlot.transform.position);
            if (slot == null)
            {
                return;
            }
            var item = GetReadyItem();
            _energyProvider.ChangeValue(-1);

            slot.DropItem(item);
            item.MoveToParent().Forget();
        }

        private Item GetReadyItem()
        {
            var item = _itemFactory.GetItem();

            var newConfig = _itemConfigsFactory.GetConfig(_weightedRandomSelector.Choose(_items));
            item.Setup(newConfig);

            item.transform.position = Item.transform.position;
            return item;
        }

        public override SystemData GetSystemData()
        {
            var data = base.GetSystemData();
            data.Description = ConstStrings.TAP_TO_GET_ITEMS + " " + data.Description;
            data.Sprites.Add(Item.Config.Sprite);
            foreach (var weightedItem in _items)
            {
                data.Sprites.Add(weightedItem.Item.Sprite);
            }
            return data;
        }
    }
}