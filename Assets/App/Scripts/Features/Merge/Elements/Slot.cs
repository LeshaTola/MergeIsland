using App.Scripts.Features.Merge.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Features.Merge.Elements
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        private MergeResolver _mergeResolver;
        
        public Item Item { get; private set; }

        public void Initialize(MergeResolver mergeResolver)
        {
            _mergeResolver = mergeResolver;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<Item>();

            if (Item != null)
            {
                TryMerge(item);
                return;
            }

            DropItem(item);
        }

        public void Clear()
        {
            Item = null;
        }

        public void DropItem(Item item)
        {
            item.ChangeSlot(this);
            Item = item;
        }

        private void TryMerge(Item item)
        {
            if (!_mergeResolver.TryMerge(Item, item, out var mergeResult))
            {
                SwapItems(item);
            }
            else
            {
                Item.Setup(mergeResult);
                item.Release();
            }
        }

        private void SwapItems(Item item)
        {
            var fromSlot = item.CurrentSlot;
            var currentItem = Item;
            currentItem.ChangeSlot(null);
            
            DropItem(item);

            fromSlot.DropItem(currentItem);
            currentItem.MoveToParent();
        }

    }
}