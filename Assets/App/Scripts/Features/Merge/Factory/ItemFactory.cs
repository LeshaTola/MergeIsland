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

        public ItemFactory(IPool<Item> itemPool, 
            Transform overlayParent,
            SelectionProvider selectionProvider)
        {
            _itemPool = itemPool;
            _overlayParent = overlayParent;
            _selectionProvider = selectionProvider;
        }
        
        public Item GetItem()
        {
            var item = _itemPool.Get();
            item.Initialize(_overlayParent,_selectionProvider);
            return item; 
        }
    }
}