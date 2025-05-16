using App.Scripts.Features.Merge.Configs;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Elements
{
    public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPoolableObject<Item>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        
        private Transform _overlayParent;
        private IPool<Item> _pool;

        public Slot CurrentSlot { get; private set; }
        public string Id {get; private set;}
        
        public void Initialize(Transform overlayParent)
        {
            _overlayParent = overlayParent;
        }

        public void Setup(ItemConfig config)
        {
            Id = config.Id;
            _image.sprite = config.Sprite;
        }
        
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

        public void OnGet(IPool<Item> pool)
        {
            _pool = pool;
        }

        public void Release()
        {
            _pool.Release(this);
        }

        public void OnRelease()
        {
            CurrentSlot?.Clear();
        }
    }
}