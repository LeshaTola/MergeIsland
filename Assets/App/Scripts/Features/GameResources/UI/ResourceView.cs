using System;
using App.Scripts.Modules.Screens;
using TMPro;
using UnityEngine;

namespace App.Scripts.Features.GameResources.UI
{
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using System;

    public class ResourceView : GameScreen
    {
        public event Action OnAddClicked;

        [SerializeField] private TextMeshProUGUI _valueText;

        private Tween _valueTween;
        private int _currentValue;

        public void Setup(int value)
        {
            _valueTween?.Kill();
            _valueTween = DOVirtual.Int(_currentValue, value, 0.3f, v =>
            {
                _currentValue = v;
                _valueText.text = v.ToString();
            }).SetEase(Ease.OutQuad);
        }
    }

}