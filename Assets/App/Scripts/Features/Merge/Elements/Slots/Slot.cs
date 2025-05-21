using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Services;
using App.Scripts.Features.Merge.Services.Hand;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Features.Merge.Elements.Slots
{
    public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] public SlotVisual Visual { get; private set; }

        private MergeResolver _mergeResolver;
        private HandProvider _handProvider;

        public Item Item { get; private set; }
        public bool IsSelected { get; private set; }

        public void Initialize(MergeResolver mergeResolver, HandProvider handProvider)
        {
            _mergeResolver = mergeResolver;
            _handProvider = handProvider;
        }

        public void SetSelected(bool isSelected)
        {
            IsSelected = isSelected;
            Visual.ShowSelector(IsSelected);
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            var takenItem = _handProvider.TakenItem;
            if (takenItem == null || Item == null || takenItem == Item)
            {
                return;
            }

            if (takenItem.Config.Id.Equals(Item.Config.Id))
            {
                Visual.ShowMergeHint();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Visual.HideMergeHint();
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
            Visual.HideMergeHint();
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