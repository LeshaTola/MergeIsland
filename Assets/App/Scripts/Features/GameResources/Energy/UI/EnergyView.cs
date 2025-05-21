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

        public void UpdateUI(int currentValue, int maxValue)
        {
            _energyText.text = $"{currentValue}/{maxValue}";

            _recoveringTimerText.gameObject.SetActive(currentValue < maxValue);

            float value = CalculateSliderValue(currentValue, maxValue);
            _fillImage.fillAmount = value;
        }

        public void UpdateTimer(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            _recoveringTimerText.text = timeString;
        }

        private static float CalculateSliderValue(int currentValue, int maxValue)
        {
            float value = (float) currentValue / maxValue;
            value = Mathf.Clamp01(value);
            return value;
        }
    }
}