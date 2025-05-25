using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using YG;

namespace App.Scripts.Features.Advertisement.Providers
{
    public class AdvertisementProvider
    {
        private readonly AdvertisementConfig _config;
        private CancellationTokenSource _cooldownTokenSource;

        public bool IsCanShowAd { get; set; }
        public bool IsReady { get; private set; } = true;

        public AdvertisementProvider(AdvertisementConfig config)
        {
            _config = config;
            IsCanShowAd = YG2.saves.IsCanShowAd;
        }

        public void ShowInterstitialAd()
        {
            if (IsCanShowAd)
            {
                YG2.InterstitialAdvShow();
            }
        }

        public void ShowRewardedAd(string id)
        {
            if (!IsReady) return;

            YG2.RewardedAdvShow(id);
            _ = StartCooldown();
        }

        private async UniTaskVoid StartCooldown()
        {
            _cooldownTokenSource?.Cancel();
            _cooldownTokenSource = new CancellationTokenSource();

            IsReady = false;
            try
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(_config.InterstitialCooldown),
                    cancellationToken: _cooldownTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            finally
            {
                IsReady = true;
            }
        }
    }
}