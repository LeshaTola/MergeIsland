using System;

namespace App.Scripts.Features.GameResources.Energy.Saves
{
    [Serializable]
    public class EnergyData
    {
        public DateTime ExitTime;
        public float RemainingRecoveryTime;
        public int Energy;
    }
}