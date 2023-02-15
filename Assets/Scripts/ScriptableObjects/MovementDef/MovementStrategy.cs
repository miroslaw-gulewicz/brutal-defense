using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovmentStrategy", menuName = "ScriptableObjects/MovmentStrategy")]
public class MovementStrategy : ScriptableObject
{
    public virtual void OnMoveTick(IMovable movable)
    {
        movable.Rigidbody.transform.position = 
            Vector3.MoveTowards(movable.Rigidbody.transform.position, movable.Destination, Time.deltaTime * movable.Speed);
    }

    public interface IMovable
    {
        GameObject Rigidbody { get; }
        float Speed { get;}
        Vector3 Destination { get; }    
    }
}