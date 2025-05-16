using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.Features.Merge.Grid
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        private MergeItem _mergeItem;

        public void OnDrop(PointerEventData eventData)
        {
            if (_mergeItem != null)
            {
                TryMergeItems();
                return;
            }

            DropItem(eventData);
        }

        private void TryMergeItems()
        {
            /*if ()
            {

            }*/
        }

        private void DropItem(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<MergeItem>();
            item.ChangeSlot(this);
            _mergeItem = item;
        }

        public void Clear()
        {
            _mergeItem = null;
        }
    }
}