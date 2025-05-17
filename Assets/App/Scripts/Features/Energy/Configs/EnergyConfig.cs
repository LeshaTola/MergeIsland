using UnityEngine;

namespace App.Scripts.Features.Energy.Configs
{
	[CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/Energy")]
	public class EnergyConfig : ScriptableObject
	{
		[field: SerializeField] public int MaxEnergy { get; private set; }
		[field: SerializeField] public float RecoveryTime { get; private set; }
		[field: SerializeField] public int RecoveryValue { get; private set; }
	}
}
