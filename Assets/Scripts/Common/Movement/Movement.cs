using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementStrategy;
using static PathMovementStrategy;

public class Movement : MonoBehaviour, IMovable
{
	[SerializeField] private Vector3 destination;

	[SerializeField] private MovementStrategy movementStrategy;

	[SerializeField] private Animator animator;

	[SerializeField] private Agent _agent;

	[SerializeField] private float _speed;


	private void Awake()
	{
		_agent = GetComponent<Agent>();
	}

	public void Initialize(Movement template)
	{
		Path = template.Path;
		Distance = template.Distance;
		movementStrategy = template.movementStrategy;
		Initialize();
	}

	public void Initialize()
	{
		if (_agent.BasicStats != null)
		{
			_agent.BasicStats.MovingSpeedUpdated += OnMovingSpeedUpdated;
			OnMovingSpeedUpdated();
		}
	}

	private void Update()
	{
		movementStrategy.OnMoveTick(this);
	}

	private void OnMovingSpeedUpdated()
	{
		_speed = _agent.BasicStats.Speed;
	}

	public void FaceDirection(bool isLeft)
	{
		animator.SetFloat("Blend", isLeft ? -1 : 1);
	}


	public float Speed
	{
		get => _speed;
		set => _speed = value;
	}

	public VertexPath Path { get; set; }
	public float Distance { get; set; }

	public GameObject MovableObject => this.gameObject;

	public Vector3 Destination
	{
		get => destination;
		set => destination = value;
	}

	Vector3 IMovable.Destination
	{
		get => destination;
		set => destination = value;
	}

	public MovementStrategy MovementStrategy
	{
		get => movementStrategy;
		set => movementStrategy = value;
	}
}