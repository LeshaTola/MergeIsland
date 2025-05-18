using System.Linq;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements;
using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Services.SelectionProviders;
using App.Scripts.Modules.ObjectPool.Pools;
using UnityEngine;

namespace App.Scripts.Features.Merge.Factory
{
    public class ItemFactory
    {
        private readonly IPool<Item> _itemPool;
        private readonly Transform _overlayParent;
        private readonly SelectionProvider _selectionProvider;
        private readonly ItemConfigsFactory _itemConfigsFactory;

        public ItemFactory(IPool<Item> itemPool, 
            Transform overlayParent,
            SelectionProvider selectionProvider, ItemConfigsFactory itemConfigsFactory)
        {
            _itemPool = itemPool;
            _overlayParent = overlayParent;
            _selectionProvider = selectionProvider;
            _itemConfigsFactory = itemConfigsFactory;
        }
        
        public Item GetItem()
        {
            var item = _itemPool.Get();
            item.Initialize(_overlayParent,_selectionProvider);
            return item; 
        }

        public Item GetItem(ItemConfig itemConfig, bool isInWeb)
        {
            var item = GetItem();
            var config = _itemConfigsFactory.GetConfig(itemConfig, isInWeb);
            item.Setup(config);
            return item;
        }

        public void HideAll()
        {
            var items = _itemPool.Active.ToList();
            foreach (var item in items)
            {
                item.Release();
            }
        }
    }
}