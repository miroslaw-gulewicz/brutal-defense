using System;
using UnityEngine;
using static WayPointMovementStrategy;

[Serializable]
public class WayPointManager
{
    [SerializeField]
    WayPoint[] waypoints;
    
    [SerializeField]
    Mode mode = Mode.LOOP;

    int _currentWayPointIndex = 0;
    int _wayPointDirection = 1;

    public void Reset()
    {
        _currentWayPointIndex = 0;
    }

    public bool ISNextWayPointReached(IWayPoint wayPoint) =>  
         UnityEngine.Object.ReferenceEquals(wayPoint, waypoints[_currentWayPointIndex]);
    

    public void SetNextPoint()
    {     
        var nextWayPointPositionIndex = _currentWayPointIndex + _wayPointDirection;

        if (nextWayPointPositionIndex < waypoints.Length && nextWayPointPositionIndex > -1)
        {
            _currentWayPointIndex = nextWayPointPositionIndex;
            return;
        }

        switch (mode)
        {
            case Mode.LOOP:
                _currentWayPointIndex = 0;
                break;
            case Mode.PINGPONG:
                _wayPointDirection *= -1;
                _currentWayPointIndex += _wayPointDirection;
                break;
        }
    }

    public IWayPoint Current => waypoints.Length > 0 ? waypoints[_currentWayPointIndex] : null;

    public WayPoint[] Waypoints { get => waypoints; set
        {
            waypoints = value;
            Reset();
        }
    }

    internal void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0);

        for (int i = 0; i < waypoints.Length; i++)
        {           
            if(i < waypoints.Length - 1)
                Gizmos.DrawLine(waypoints[i].Position, waypoints[i + 1].Position);
            else if (i == waypoints.Length - 1 && mode == Mode.LOOP)
            {
                Gizmos.DrawLine(waypoints[i].Position, waypoints[0].Position);
            }
        }
    }

    [Serializable]
    public enum Mode
    {
        LOOP, PINGPONG
    }
}
