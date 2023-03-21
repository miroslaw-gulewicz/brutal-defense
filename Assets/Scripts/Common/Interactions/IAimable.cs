using UnityEngine;

public interface IAimable
{
	GameObject Target { get; set; }

	System.Collections.Generic.IEnumerable<GameObject> Targets { get; set; }
}