using System.Collections.Generic;
using System.Linq;
using System.Threading;
using App.Scripts.Modules.PopupAndViews.Animations;
using App.Scripts.Modules.PopupAndViews.General.Controllers;
using App.Scripts.Modules.Sounds;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Modules.PopupAndViews.General.Popup
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class Popup : MonoBehaviour, IPopup
    {
        [FoldoutGroup("General")]
        [SerializeReference]
        private IAnimation _popupAnimation;

        [FoldoutGroup("General")]
        [SerializeField] protected AudioDatabase _audioDatabase;

        [FoldoutGroup("General")]
        [SerializeField]
        protected GraphicRaycaster _raycaster;

        [FoldoutGroup("General")]
        [SerializeField]
        protected Canvas _canvas;

        public IPopupController Controller { get; private set; }
        public Canvas Canvas => _canvas;
        public bool Active { get; private set; }

        private CancellationTokenSource _cancellationTokenSource;

        public void Initialize(IPopupController controller)
        {
            Controller = controller;
        }

        public virtual async UniTask Hide()
        {
            Deactivate();

            Cleanup();
            _cancellationTokenSource = new CancellationTokenSource();
            await _popupAnimation.PlayHideAnimation(gameObject, _cancellationTokenSource.Token);

            Controller.RemoveActivePopup(this);
            gameObject.SetActive(false);
        }

        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);
            Controller.AddActivePopup(this);

            Cleanup();
            _cancellationTokenSource = new CancellationTokenSource();
            await _popupAnimation.PlayShowAnimation(gameObject, _cancellationTokenSource.Token);

            Activate();
        }

        public void Activate()
        {
            Active = true;
            _raycaster.enabled = true;
        }

        public void Deactivate()
        {
            Active = false;
            _raycaster.enabled = false;
        }

        private void Cleanup()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public List<string> GetAudioKeys()
        {
            if (_audioDatabase == null)
            {
                return null;
            }

            return _audioDatabase.Audios.Keys.ToList();
        }
    }
}