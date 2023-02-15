using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProximityAffector : IProximityInflictor
{
    [SerializeField] private int _executionNumber;
    [SerializeField] private int _intervalBetween;

    private HashSet<IAffected> _affectedsInRange = new HashSet<IAffected>();

    public void Start()
    {
        StartCoroutine(TimerCoorutine());
    }
    private IEnumerator TimerCoorutine()
    {
        while(_executionNumber > 0)
        {
            foreach (var item in _affectedsInRange)
            {
                item.ApplyEffect(_inflictors);
            }
            yield return new WaitForSeconds(_intervalBetween);
        }
    }

    protected override void TriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IAffected>(out IAffected affector))
        {
            _affectedsInRange.Add(affector);
            Debug.Log("Object Affected " + other);
        }
    }

    protected override void TriggerExit(Collider other)
    {
        if (other.TryGetComponent<IAffected>(out IAffected affector))
        {
            affector.CancelEffect(_inflictors);
            _affectedsInRange.Remove(affector);
            Debug.Log("Object exited " + other);
        }
    }
}
