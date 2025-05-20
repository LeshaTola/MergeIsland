using System;
using System.Collections.Generic;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Elements.Buttons;
using App.Scripts.Modules.Localization.Elements.Texts;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.Screens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Merge.Screens
{
    public class InfoPanel : GameScreen
    {
        public event Action OnButtonClick;

        [Header("General")]
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _imageObject;
        [SerializeField] private TMPLocalizer _nameText;
        [SerializeField] private LocalizedWithValue _levelText;
        
        [Header("Description")]
        [SerializeField] private Image _blockImage; 
        [SerializeField] private TMPLocalizer _descriptionText;
        [SerializeField] private GameObject _craftPanel; 
        [SerializeField] private List<Image> _craftImages; 
        [SerializeField] private TextMeshProUGUI _timerText;

        [Header("Action")]
        [SerializeField] private GameObject _actionPanel; 
        [SerializeField] private TMPLocalizer _actionText;
        [SerializeField] private Image _actionImage; 
        [SerializeField] private TMPLocalizedButton _actionButton;

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            _nameText.Initialize(localizationSystem);
            _levelText.Initialize(localizationSystem);
            _descriptionText.Initialize(localizationSystem);

            _actionText.Initialize(localizationSystem);
            _actionButton.Initialize(localizationSystem);
            Initialize();
        }

        public override void Initialize()
        {
            _actionButton.UpdateAction(() => OnButtonClick?.Invoke());
        }

        public void Setup(ItemConfig itemConfig)
        {
            Default();
            SetupGeneral(itemConfig.Sprite, itemConfig.Name, itemConfig.Level);
            if (itemConfig.System != null)
            {
                SetupSystem(itemConfig.System.GetSystemData());
            }
        }

        public override void Default()
        {
            _imageObject.SetActive(false);
            _nameText.Text.text = string.Empty;
            _levelText.Setup(string.Empty,string.Empty);
            _descriptionText.Text.text = string.Empty;
            _craftPanel.SetActive(false);
            foreach (var image in _craftImages)
            {
                image.gameObject.SetActive(false);
            }
            _blockImage.gameObject.SetActive(false);
            _timerText.gameObject.SetActive(false);
            _actionPanel.SetActive(false);
        }

        public override void Cleanup()
        {
            _nameText.Cleanup();
            _levelText.Cleanup();
            _descriptionText.Cleanup();

            _actionText.Cleanup();
            _actionButton.Cleanup();
        }

        private void SetupGeneral(Sprite sprite, string itemName, int level)
        {
            _image.sprite = sprite;
            _nameText.Key = itemName;
            _levelText.Setup("level: ", (level + 1).ToString());
        
            _nameText.Translate();
        }

        private void SetupSystem(SystemData systemData)
        {
            if (systemData.IsBlocked)
            {
                _blockImage.gameObject.SetActive(true);
            }
            else
            {
                SetupCraft(systemData);
                if (systemData.Timer > 0)
                {
                    _timerText.gameObject.SetActive(true);
                    _timerText.text = systemData.Timer.ToString();
                }
                
                _descriptionText.Key = systemData.Description;
                _descriptionText.Translate();
            }
            
            SetupAction(systemData.ActionData);
        }

        private void SetupCraft(SystemData systemData)
        {
            if (systemData.Sprites is not {Count: > 0})
            {
                return;
            }
            
            _craftPanel.SetActive(true);
            for (int i = 0; i < systemData.Sprites.Count; i++)
            {
                var sprite = systemData.Sprites[i];
                var craftImage = _craftImages[i];
                        
                craftImage.gameObject.SetActive(true);
                craftImage.sprite = sprite;
            }
        }

        private void SetupAction(ActionData actionData)
        {
            if (actionData == null)
            {
                return;
            }
            
            _actionPanel.SetActive(true);
            _actionText.Key = actionData.ActionText;
            _actionImage.sprite = actionData.ActionImage;
            _actionButton.UpdateText(actionData.ButtonText);
        
            _actionText.Translate();
            _actionButton.Translate();
        }
    }
}