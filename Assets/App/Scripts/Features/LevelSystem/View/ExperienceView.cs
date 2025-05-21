using App.Scripts.Modules.Screens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.LevelSystem.View
{
    public class ExperienceView : GameScreen
    {
        [SerializeField] private TextMeshProUGUI _xpText;
        [SerializeField] private Image _fillImage;

        public void SetExperience(int current, int max)
        {
            _xpText.text = $"{current}/{max}";
            _fillImage.fillAmount = max > 0 ? (float)current / max : 0f;
        }
    }
}