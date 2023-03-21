using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovmentStrategy", menuName = "ScriptableObjects/MovmentStrategy")]
public class MovementStrategy : ScriptableObject
{
	public virtual void OnMoveTick(IMovable movable)
	{
		movable.MovableObject.transform.position =
			Vector3.MoveTowards(movable.MovableObject.transform.position, movable.Destination,
				Time.deltaTime * movable.Speed);
	}

	public interface IMovable
	{
		GameObject MovableObject { get; }
		float Speed { get; }
		Vector3 Destination { get; set; }

		float Distance { get; set; }

		public VertexPath Path { get; set; }
	}
}