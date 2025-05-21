using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Features.LevelSystem.Configs
{
    [CreateAssetMenu(fileName = "ExperienceConfig", menuName = "Configs/Experience Config")]
    public class ExperienceConfig : ScriptableObject
    {
        public List<LevelConfig> Levels;

        public int GetTotalLevels()
        {
            return Levels.Count;
        }

        public LevelConfig GetLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= Levels.Count)
                return null;

            return Levels[levelIndex];
        }
    }
}