using UnityEngine;
using UnityEngine.Events;
using static MovementStrategy;
using static PathMovementStrategy;
using static WayPointManager;

[RequireComponent(typeof(Collider))]
public class WayPoint : MonoBehaviour, IWayPoint
{
	[SerializeField] public UnityEvent OnWayPointReached;
	public Vector3 Position => transform.position;

	public WayPoint NextPoint
	{
		get => _nextPoint;
		set => _nextPoint = value;
	}

	Vector3 IWayPoint.NextPoint
	{
		get => _nextPoint.transform.position;
	}

	public WayPoint _nextPoint;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IMovable movable))
		{
			movable.Destination = _nextPoint.Position;
			OnWayPointReached.Invoke();
		}
	}
}