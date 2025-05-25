using System;
using System.Collections.Generic;
using App.Scripts.Features.GameResources.Configs;
using App.Scripts.Features.LevelSystem.Configs;

namespace App.Scripts.Features.LevelSystem.Services
{
    public class ExperienceService
    {
        public event Action<int, int> OnExperienceChanged;
        public event Action<int, List<RewardConfig>> OnLevelUp;

        private readonly ExperienceConfig _config;

        public int CurrentExperience { get; private set; }

        public int CurrentLevel { get; private set; }

        public int ExperienceToNextLevel => HasNextLevel ? _config.Levels[CurrentLevel].ExperienceRequired : 0;
        public bool HasNextLevel => CurrentLevel < _config.Levels.Count;

        public ExperienceService(ExperienceConfig config)
        {
            _config = config;
        }

        public void AddExperience(int amount)
        {
            if (!HasNextLevel)
                return;

            CurrentExperience += amount;

            while (HasNextLevel && CurrentExperience >= _config.Levels[CurrentLevel].ExperienceRequired)
            {
                CurrentExperience -= _config.Levels[CurrentLevel].ExperienceRequired;
                var rewards = _config.Levels[CurrentLevel].Rewards;
                CurrentLevel++;

                OnLevelUp?.Invoke(CurrentLevel, rewards);
            }

            NotifyChange();
        }

        public void Reset()
        {
            CurrentExperience = 0;
            CurrentLevel = 0;
            NotifyChange();
        }

        private void NotifyChange()
        {
            OnExperienceChanged?.Invoke(CurrentExperience, ExperienceToNextLevel);
        }
    }
}