using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.Features.Merge.Elements.Items
{
    public class ItemAnimator : MonoBehaviour
    {
        [SerializeField] private float _moveAnimationDuration = 0.5f;
        [SerializeField] private float _bounceAnimationDuration = 0.4f;
        [SerializeField] private float _mergeAnimationDuration = 0.3f;

        private Tween _currentTween;
        
        public async UniTask MoveTo(Vector3 targetPosition)
        {
            _currentTween?.Complete();

            _currentTween = transform.DOMove(targetPosition, _moveAnimationDuration)
                .SetEase(Ease.InOutQuad);

            await _currentTween.ToUniTask();
        }

        public async UniTask BounceAnimation()
        {
            _currentTween?.Complete();

            float half = _bounceAnimationDuration / 2f;

            _currentTween = DOTween.Sequence()
                .Append(transform.DOScale(0.75f, half).SetEase(Ease.OutQuad))
                .Append(transform.DOScale(1f, half).SetEase(Ease.OutBack));

            await _currentTween.ToUniTask();
        }

        public async UniTask MergeAnimation()
        {
            _currentTween?.Complete();

            transform.localScale = Vector3.zero;
            _currentTween = transform.DOScale(1f, _mergeAnimationDuration).SetEase(Ease.OutBack);

            await _currentTween.ToUniTask();
        }
    }
}