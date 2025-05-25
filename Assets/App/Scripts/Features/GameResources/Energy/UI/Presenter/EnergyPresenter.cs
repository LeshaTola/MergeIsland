using App.Scripts.Features.GameResources.Energy.Providers;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;

namespace App.Scripts.Features.GameResources.Energy.UI.Presenter
{
    public class EnergyPresenter : IInitializable, ICleanupable
    {
        private readonly EnergyView _energySliderUI;
        private readonly EnergyProvider _energyProvider;

        public EnergyPresenter(EnergyView energySliderUI, EnergyProvider energyProvider)
        {
            _energySliderUI = energySliderUI;
            _energyProvider = energyProvider;
        }

        public void Initialize()
        {
            _energyProvider.OnValueChanged += UpdateUI;
            _energyProvider.OnEnergyTimerChanged += UpdateTimer;
            UpdateUI(_energyProvider.Value);
        }

        public void Cleanup()
        {
            _energyProvider.OnValueChanged -= UpdateUI;
            _energyProvider.OnEnergyTimerChanged -= UpdateTimer;
        }

        private void UpdateUI(int value)
        {
            _energySliderUI.UpdateUI(value, _energyProvider.Config.MaxEnergy);
        }

        private void UpdateTimer()
        {
            _energySliderUI.UpdateTimer((int) _energyProvider.RemainingRecoveryTime);
        }
    }
}