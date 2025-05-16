using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Grid
{
    public class MergeItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;

        [SerializeField] private Transform _overlayParent;

        public Slot CurrentSlot { get; private set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_overlayParent, false);
            _image.raycastTarget = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            MoveToParent();
            _image.raycastTarget = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _rectTransform,
                eventData.position,
                Camera.main,
                out Vector3 worldPoint);

            _rectTransform.position = worldPoint;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void ChangeSlot(Slot newSlot)
        {
            CurrentSlot?.Clear();
            CurrentSlot = newSlot;
        }

        public void MoveToParent()
        {
            transform.SetParent(CurrentSlot.transform, false);
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}