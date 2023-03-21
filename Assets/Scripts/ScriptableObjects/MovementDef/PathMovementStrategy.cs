using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PathMovementStrategy", menuName = "ScriptableObjects/PathMovementStrategy")]
public class PathMovementStrategy : MovementStrategy
{
	public override void OnMoveTick(IMovable movable)
	{
		movable.Distance += movable.Speed * Time.deltaTime;
		movable.MovableObject.transform.position = movable.Path.GetPointAtDistance(movable.Distance);
	}
}