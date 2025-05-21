using System;
using System.Collections.Generic;
using App.Scripts.Features.GameResources.Configs;

namespace App.Scripts.Features.LevelSystem.Configs
{
    [Serializable]
    public class LevelConfig
    {
        public int ExperienceRequired;
        public List<RewardConfig> Rewards;
    }
}