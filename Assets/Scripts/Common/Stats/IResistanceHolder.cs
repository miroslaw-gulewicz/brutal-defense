using System.Collections.Generic;

public interface IResistanceHolder
{
    public float this[IDestructable.DamageType index] { get; }
}
