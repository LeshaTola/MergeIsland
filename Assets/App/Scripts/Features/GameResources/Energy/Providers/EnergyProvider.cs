using System;
using App.Scripts.Features.GameResources.Energy.Configs;
using App.Scripts.Features.GameResources.Energy.Saves;
using App.Scripts.Features.GameResources.Providers;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;

namespace App.Scripts.Features.GameResources.Energy.Providers
{
    public class EnergyProvider : ResourceProvider, IInitializable, IUpdatable
    {
        public event Action OnEnergyTimerChanged;

        private readonly ITimeProvider _timeProvider;
        private readonly EnergyConfig _config;
        private readonly IDataProvider<EnergyData> _energyDataProvider;

        private float _timer;

        public EnergyConfig Config => _config;

        public float RemainingRecoveryTime
        {
            get => _timer;
            set => _timer = value;
        }

        public EnergyProvider(EnergyConfig config, ITimeProvider timeProvider,
            IDataProvider<EnergyData> energyDataProvider) : base(config.ResourceConfig)
        {
            _config = config;
            _timeProvider = timeProvider;
            _energyDataProvider = energyDataProvider;
        }

        public void Initialize()
        {
            LoadData();
        }

        public void Update()
        {
            ProcessTimer();
        }

        public override void ChangeValue(int energy)
        {
            base.ChangeValue(energy);
            SaveData();
        }

        public void SaveData()
        {
            var data = new EnergyData
            {
                Energy = Value,
                RemainingRecoveryTime = RemainingRecoveryTime,
                ExitTime = DateTime.Now
            };

            _energyDataProvider.SaveData(data);
        }

        public void LoadData()
        {
            var data = _energyDataProvider.GetData() ?? CreateInitialData();
            _energyDataProvider.SaveData(data);

            int totalEnergy = data.Energy;
            int additional = CalculateOfflineEnergy(data, totalEnergy);
            totalEnergy += additional;

            Value = 0;
            ChangeValue(totalEnergy);
        }

        private EnergyData CreateInitialData()
        {
            return new EnergyData
            {
                Energy = _config.MaxEnergy,
                RemainingRecoveryTime = 0,
                ExitTime = DateTime.Now
            };
        }

        private int CalculateOfflineEnergy(EnergyData data, int totalEnergy)
        {
            if (totalEnergy >= _config.MaxEnergy)
                return 0;

            var timeSinceExit = DateTime.Now - data.ExitTime;
            float timeLeft = GetRemainingTimeAfterExit(data.RemainingRecoveryTime, timeSinceExit);

            if (timeLeft >= 0)
            {
                RemainingRecoveryTime = timeLeft;
                return 0;
            }

            int recoveredEnergy = CalculateRecoveredEnergy(-timeLeft);
            RemainingRecoveryTime = CalculateNextRecoveryTimer(-timeLeft);

            return LimitToMaxCapacity(recoveredEnergy, totalEnergy);
        }

        private float GetRemainingTimeAfterExit(float savedRemainingTime, TimeSpan timePassed)
        {
            return savedRemainingTime - (float) timePassed.TotalSeconds;
        }

        private int CalculateRecoveredEnergy(float overtimeSeconds)
        {
            int fullRecoveries = (int) (overtimeSeconds / _config.RecoveryTime);
            return (fullRecoveries + 1) * _config.RecoveryValue;
        }

        private float CalculateNextRecoveryTimer(float overtimeSeconds)
        {
            return _config.RecoveryTime - (overtimeSeconds % _config.RecoveryTime);
        }

        private int LimitToMaxCapacity(int recoveredEnergy, int currentEnergy)
        {
            int energyNeeded = _config.MaxEnergy - currentEnergy;
            return Math.Min(recoveredEnergy, energyNeeded);
        }

        private void ProcessTimer()
        {
            if (IsAtMaxEnergy())
            {
                ResetTimer();
                return;
            }

            UpdateTimer();

            if (_timer > 0f)
            {
                return;
            }

            ResetTimer();
            TryRecoverEnergy();
        }

        private bool IsAtMaxEnergy()
        {
            return Value >= _config.MaxEnergy;
        }

        private void ResetTimer()
        {
            RemainingRecoveryTime = _config.RecoveryTime;
            _timer = _config.RecoveryTime;
        }

        private void UpdateTimer()
        {
            _timer -= _timeProvider.DeltaTime;
            OnEnergyTimerChanged?.Invoke();
        }

        private void TryRecoverEnergy()
        {
            if (Value < _config.MaxEnergy)
            {
                ChangeValue(_config.RecoveryValue);
            }
        }
    }
}