namespace App.Scripts.Modules.TimeProvider
{
	public interface ITimeProvider
	{
		public float TimeMultiplier { get; set; }
		public float DeltaTime { get; }
		public float FixedDeltaTime { get; }
	}
}
