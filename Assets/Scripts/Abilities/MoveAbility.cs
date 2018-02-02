using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAbility : Ability
{
    public override void Execute(Target target)
    {
        Vector3 destination = target.GetLocationTarget();

        if (destination == null)
        {
            Debug.LogError("Target destination cannot be null.");
        }

        NavMeshAgent agent = this.owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination);
    }
}
