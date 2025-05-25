using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.GameResources.Energy.UI
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private TextMeshProUGUI _recoveringTimerText;

        private Tween _uiTween;
        private int _currentDisplayedValue;

        public void UpdateUI(int currentValue, int maxValue)
        {
            _recoveringTimerText.gameObject.SetActive(currentValue < maxValue);

            _uiTween?.Kill();
            _uiTween = DOVirtual.Float(_currentDisplayedValue, currentValue, 0.3f, value =>
                {
                    int animatedValue = Mathf.RoundToInt(value);
                    _energyText.text = $"{animatedValue}/{maxValue}";
                    _currentDisplayedValue = animatedValue;
                    _fillImage.fillAmount  = CalculateSliderValue(value, maxValue);
                })
                .OnComplete(() => _currentDisplayedValue = currentValue)
                .SetEase(Ease.OutQuad);
        }

        public void UpdateTimer(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            _recoveringTimerText.text = timeString;
        }

        private static float CalculateSliderValue(float currentValue, int maxValue)
        {
            float value = currentValue / maxValue;
            value = Mathf.Clamp01(value);
            return value;
        }
    }
}