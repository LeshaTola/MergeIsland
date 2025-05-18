using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Elements
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Image _selector;
            
        private MergeResolver _mergeResolver;
        
        public Item Item { get; private set; }
        public bool IsSelected { get; private set; }

        public void Initialize(MergeResolver mergeResolver)
        {
            _mergeResolver = mergeResolver;
        }

        public void SetSelected(bool isSelected)
        {
            IsSelected = isSelected;
            _selector.gameObject.SetActive(IsSelected);
        }

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<Item>();
            
            if (Item != null)
            {
                if (item == Item)
                {
                    return;
                }
                TryMerge(item);
                return;
            }
            
            if (item.IsBlocked)
            {
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
                if (Item.IsBlocked)
                {
                    return;
                }   
                SwapItems(item);
            }
            else
            {
                Item.Setup(mergeResult);
                Item.Animator.MergeAnimation().Forget();
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
            currentItem.MoveToParent().Forget();
        }

    }
}