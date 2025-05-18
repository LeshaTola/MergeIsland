using System;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Services.SelectionProviders;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Elements.Items
{
    public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPoolableObject<Item>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [field: SerializeField] public ItemAnimator Animator { get; private set; }

        private Transform _overlayParent;
        private SelectionProvider _selectionProvider;
        private IPool<Item> _pool;

        private bool _isDragging;
        
        public Slot CurrentSlot { get; private set; }
        public ItemConfig Config { get; private set; }
        
        public void Initialize(Transform overlayParent, SelectionProvider selectionProvider)
        {
            _overlayParent = overlayParent;
            _selectionProvider = selectionProvider;
        }

        public void Setup(ItemConfig config)
        {
            Config = config;
            InitializeSystem();
            
            _image.sprite = config.Sprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _selectionProvider.ClearSelection();
            PlaceOnOverlay();
            _image.raycastTarget = false;
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _selectionProvider.Select(CurrentSlot);
            MoveToParent().Forget();
            _image.raycastTarget = true;
            _isDragging = false;
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
            Animator.BounceAnimation().Forget();
            if (!CurrentSlot.IsSelected)
            {
                _selectionProvider.Select(CurrentSlot);
                return;
            }
            
            Config.System?.Execute();
        }

        public void ChangeSlot(Slot newSlot)
        {
            CurrentSlot?.Clear();
            CurrentSlot = newSlot;
        }

        public async UniTask MoveToParent()
        {
            PlaceOnOverlay();
            await Animator.MoveTo(CurrentSlot.transform.position);
            if (_isDragging)
            {
                return;
            }
            transform.SetParent(CurrentSlot.transform);
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

        private void PlaceOnOverlay()
        {
            transform.SetParent(_overlayParent);
        }

        private void InitializeSystem()
        {
            if (Config.System != null)
            {
                Config.System.Item = this;
            }
        }
    }
}