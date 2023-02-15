using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightProximityTrigger : ProximityTrigger
{

    protected List<Agent> _agentsInRange = new List<Agent>();

    protected override void TriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Agent agent))
        {
            _agentsInRange.Add(agent);
            agent.HighLight(true);
        }
           
    }

    protected override void TriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Agent agent))
        {
            agent.HighLight(false);
            _agentsInRange.Remove(agent);
        }
    }

    private void OnDisable()
    {
       _agentsInRange.ForEach(agent => agent.HighLight(false)); 
    }

    private void OnDestroy()
    {
        OnDisable();
    }
}
