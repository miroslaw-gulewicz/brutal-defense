using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementStrategy;
using static WayPointMovementStrategy;

public class Movement : MonoBehaviour, IMovable, IWayPointAgent
{
    [SerializeField]
    private Vector3 destination;

    [SerializeField]
    private MovementStrategy movementStrategy;

    [SerializeField]
    private WayPointManager wayPointManager;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Agent _agent;

    [SerializeField]
    private float _speed;

    private WayPointData _data;

    private void Awake()
    {
        _agent = GetComponent<Agent>();    
    }

    public void Initialize()
    {
        _agent.BasicStats.MovingSpeedUpdated += OnMovingSpeedUpdated;
        OnMovingSpeedUpdated();
        if (wayPointManager != null && wayPointManager.Current != null)
        {
            destination = wayPointManager.Current.Position;
        }    
    }

    private void Update()
    {
        movementStrategy?.OnMoveTick(this);
    }

    private void OnMovingSpeedUpdated()
    {
        _speed = _agent.BasicStats.Speed;
    }

    void IWayPointAgent.WayPointReached(IWayPoint wayPoint)
    {
        if (wayPointManager.ISNextWayPointReached(wayPoint))
        {
            wayPointManager.SetNextPoint();
            destination = wayPointManager.Current.Position;
            var dir = destination.x < transform.position.x;
            FaceDirection(dir);
        }
    }

    public void FaceDirection(bool isLeft)
    {
        animator.SetFloat("Blend", isLeft ? -1 : 1);
    }

    private void OnDrawGizmosSelected()
    {
        wayPointManager.OnDrawGizmosSelected();
    }

    private void OnDisable()
    {
    }


    public float Speed { get => _speed; set => _speed = value; }
    public Vector3 Destination { get => destination; set => destination = value; }
    public GameObject Rigidbody => this.gameObject;
    public WayPoint[] Waypoints { set => wayPointManager.Waypoints = value;  }
    public MovementStrategy MovementStrategy { get => movementStrategy; set => movementStrategy = value; }

    public WayPointData wayPointData => _data;
}
