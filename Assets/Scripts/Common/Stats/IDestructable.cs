public interface IDestructable
{
	void TakeDamage(DamageType damageType, short amount);

	public enum DamageType
	{
		BLUNT,
		PIERCING,
		FIRE,
		ICE,
		POISION,
		INTERNAL,
		NONE,
		HEAL
	}

	public enum StatusSource
	{
		KILLED,
		SAVED,
		SPAWNED
	}
}