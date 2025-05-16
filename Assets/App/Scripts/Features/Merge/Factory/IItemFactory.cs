using App.Scripts.Features.Merge.Elements;
using App.Scripts.Modules.ObjectPool.Pools;
using UnityEngine;

namespace App.Scripts.Features.Merge.Factory
{
    public class IItemFactory
    {
        private readonly IPool<Item> _itemPool;
        private readonly Transform _overlayParent;

        public IItemFactory(IPool<Item> itemPool, Transform overlayParent)
        {
            _itemPool = itemPool;
            _overlayParent = overlayParent;
        }
        
        public Item GetItem()
        {
            var item = _itemPool.Get();
            item.Initialize(_overlayParent);
            return item; 
        }
    }
}