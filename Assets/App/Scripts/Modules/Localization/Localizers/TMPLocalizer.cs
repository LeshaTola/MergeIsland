using TMPro;
using UnityEngine;

namespace App.Scripts.Modules.Localization.Localizers
{
    public class TMPLocalizer : MonoBehaviour, ITextLocalizer
    {
        [SerializeField] private TextMeshProUGUI _text;

        private ILocalizationSystem _localizationSystem;
        private string _key = "";

        public string Key
        {
            get => _key;
            set => _key = value;
        }

        public TextMeshProUGUI Text
        {
            get => _text;
            set => _text = value;
        }

        private void OnValidate()
        {
            gameObject.TryGetComponent(out _text);
        }

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            if (this._localizationSystem != null)
            {
                return;
            }

            this._localizationSystem = localizationSystem;
            _key = _text.text;
            localizationSystem.OnLanguageChanged += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        public void Translate()
        {
            var newText = _localizationSystem.Translate(_key);
            _text.text = newText;
        }

        private void OnLanguageChanged()
        {
            Translate();
        }

        public void Cleanup()
        {
            _localizationSystem.OnLanguageChanged -= OnLanguageChanged;
        }
    }
}