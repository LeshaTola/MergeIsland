using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements.Slots;
using App.Scripts.Features.Merge.Services.Hand;
using App.Scripts.Features.Merge.Services.Selection;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Elements.Items
{
    public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler,
        IPoolableObject<Item>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;

        [field: SerializeField] public ItemVisual Visual { get; private set; }
        [field: SerializeField] public ItemAnimator Animator { get; private set; }

        private Transform _overlayParent;
        private SelectionProvider _selectionProvider;
        private HandProvider _handProvider;
        private IPool<Item> _pool;

        private bool _isDragging;

        public Slot CurrentSlot { get; private set; }
        public ItemConfig Config { get; private set; }
        public bool IsBlocked { get; set; }

        public void Initialize(Transform overlayParent,
            SelectionProvider selectionProvider,
            HandProvider handProvider)
        {
            _overlayParent = overlayParent;
            _selectionProvider = selectionProvider;
            _handProvider = handProvider;
        }

        public void Setup(ItemConfig config)
        {
            CleanupSystem();
            Config = config;
            InitializeSystem();
            
            Animator.MergeAnimation().Forget();

            _image.sprite = config.Sprite;
            Visual.SetLastLevel(config.IsLastLevel);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsBlocked)
            {
                return;
            }

            _handProvider.TakenItem = this;
            
            _selectionProvider.ClearSelectionWithoutNotification();
            PlaceOnOverlay();
            SetRaycastTarget(false);
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsBlocked)
            {
                return;
            }

            _handProvider.TakenItem = null;
            SetRaycastTarget(true);
            _selectionProvider.Select(CurrentSlot);
            MoveToParent().Forget();
            _isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsBlocked)
            {
                return;
            }

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
            if (CurrentSlot != null && !CurrentSlot.IsSelected)
            {
                _selectionProvider.Select(CurrentSlot);
                return;
            }

            if (IsBlocked)
            {
                return;
            }

            Config.System?.Execute();
        }

        public void ChangeSlot(Slot newSlot)
        {
            CurrentSlot?.Clear();
            CurrentSlot = newSlot;
        }

        public void MoveToParentImmediate()
        {
            transform.SetParent(CurrentSlot.transform);
            transform.localPosition = Vector3.zero;
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
            CleanupSystem();
            _selectionProvider?.ClearSelection();

            if (_handProvider != null)
            {
                _handProvider.TakenItem = null;
            }

            SetRaycastTarget(true);
            CurrentSlot?.Clear();
            CurrentSlot = null;
            _isDragging = false;
        }

        private void SetRaycastTarget(bool isActive)
        {
            _image.raycastTarget = isActive;
        }

        private void PlaceOnOverlay()
        {
            transform.SetParent(_overlayParent);
        }

        private void InitializeSystem()
        {
            if (Config.System == null)
            {
                return;
            }

            Config.System.Initialize(this) ;
            Config.System.Start();
        }

        private void CleanupSystem()
        {
            if (_selectionProvider != null && _selectionProvider.Selected == CurrentSlot)
            {
                _selectionProvider.ClearSelection();
            }
            
            if (Config != null)
            {
                Config.System?.Stop();
            }
        }
    }
}