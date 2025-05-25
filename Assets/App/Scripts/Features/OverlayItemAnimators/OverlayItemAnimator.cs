using App.Scripts.Features.OverlayItemAnimators.Configs;
using App.Scripts.Modules.ObjectPool.Pools;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.OverlayItemAnimators
{
    public class OverlayItemAnimator
    {
        private readonly IPool<Image> _imagePool;
        private readonly OverlayItemAnimatorConfig _config;

        public OverlayItemAnimator(IPool<Image> imagePool, OverlayItemAnimatorConfig config)
        {
            _imagePool = imagePool;
            _config = config;
        }

        public async UniTask MoveFromSellPosition(Vector2 end, Sprite sprite)
        {
            await Move(_config.SellPosition, end, sprite);
        }

        public async UniTask MoveToSellPosition(Vector2 start, Sprite sprite)
        {
            await Move(start, _config.SellPosition, sprite);
        }

        public async UniTask Move(Vector2 start, Vector2 end, Sprite sprite)
        {
            var image = GetReadyImage(start, sprite);
            await Animate(end, image);
            _imagePool.Release(image);
        }

        private async UniTask Animate(Vector2 end, Image image)
        {
            await image.transform.DOScale(Vector3.one, _config.ScaleDuration).SetEase(_config.StartScaleEase);
            await image.transform.DOMove(end, _config.MoveDuration).SetEase(_config.MoveEase);
            await image.transform.DOScale(Vector3.zero, _config.ScaleDuration).SetEase(_config.EndScaleEase);
        }

        private Image GetReadyImage(Vector2 start, Sprite sprite)
        {
            var image = _imagePool.Get();
            image.sprite = sprite;
            image.transform.localScale = Vector3.zero;
            image.transform.position = start;
            return image;
        }
    }
}