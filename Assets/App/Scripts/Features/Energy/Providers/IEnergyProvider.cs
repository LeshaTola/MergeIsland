using System;
using App.Scripts.Features.Energy.Configs;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Features.Energy.Providers
{
	public interface IEnergyProvider
	{
		event Action OnEnergyChanged;
		event Action OnEnergyTimerChanged;

		int CurrentEnergy { get; }
		EnergyConfig Config { get; }
		float RemainingRecoveryTime { get; set; }

		void ChangeEnergy(int energy);
	}
}

