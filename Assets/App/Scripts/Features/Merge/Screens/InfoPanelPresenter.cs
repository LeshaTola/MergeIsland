using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements.Slots;
using App.Scripts.Features.Merge.Services.Selection;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Screens;

namespace App.Scripts.Features.Merge.Screens
{
    public class InfoPanelPresenter : GameScreenPresenter
    {
        private readonly InfoPanel _infoPanel;
        private readonly ILocalizationSystem _localizationSystem;
        private readonly SelectionProvider _selectionProvider;

        private ItemConfig _itemConfig;

        public InfoPanelPresenter(InfoPanel infoPanel,
            ILocalizationSystem localizationSystem,
            SelectionProvider selectionProvider) : base(infoPanel)
        {
            _infoPanel = infoPanel;
            _localizationSystem = localizationSystem;
            _selectionProvider = selectionProvider;
        }

        public override void Initialize()
        {
            _infoPanel.Initialize(_localizationSystem);
        
            _infoPanel.OnButtonClick += OnButtonClick;
            _selectionProvider.OnSlotSelected += OnSlotSelected;
        }

        public override void Cleanup()
        {
            base.Cleanup();
        
            _infoPanel.OnButtonClick -= OnButtonClick;
            _selectionProvider.OnSlotSelected -= OnSlotSelected;
        }

        private void Setup(ItemConfig itemConfig)
        {
            Default();
            _itemConfig = itemConfig;
            if (_itemConfig.System != null)
            {
                _itemConfig.System.OnValueChanged += OnValueChanged;
            }
            _infoPanel.Setup(itemConfig);
        }

        public override void Default()
        {
            if (_itemConfig == null|| _itemConfig.System == null)
            {
                return;
            }
            
            _itemConfig.System.OnValueChanged -= OnValueChanged;
        }

        private void OnButtonClick()
        {
            //ItemConfig execute action
        }

        private void OnSlotSelected(Slot slot)
        {
            Setup(slot.Item.Config);
        }

        private void OnValueChanged()
        {
            
        }
    }
}