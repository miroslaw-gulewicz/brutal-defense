using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WayPointMovementStrategy", menuName = "ScriptableObjects/WayPointMovementStrategy")]
public class WayPointMovementStrategy : MovementStrategy
{

    public override void OnMoveTick(IMovable movable)
    {
        base.OnMoveTick(movable);
    }

    public interface IWayPointAgent
    {
        void WayPointReached(IWayPoint wayPoint);
        WayPointData wayPointData { get; }
    }

   public struct WayPointData
    {

        [SerializeField]
        public Mode mode;

        public int _currentWayPointIndex;
        public int _wayPointDirection;

        [SerializeField]
        public WayPoint[] waypoints;

        public WayPointData(Mode mode, int currentWayPointIndex, int wayPointDirection, WayPoint[] waypoints)
        {
            this.mode = mode;
            _currentWayPointIndex = currentWayPointIndex;
            _wayPointDirection = wayPointDirection;
            this.waypoints = waypoints;
        }
    }

    public interface IWayPoint
    {
        Vector3 Position { get; }
    }


    [Serializable]
    public enum Mode
    {
        LOOP, PINGPONG
    }
}