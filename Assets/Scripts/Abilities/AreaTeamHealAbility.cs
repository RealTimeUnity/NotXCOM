using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AreaTeamHealAbility : Ability
{
    public float HealAmount = 0.35F;
    public override void Execute(Target target)
    {
        base.Execute(target);
        Character destination = target.GetCharacterTarget();

        NavMeshAgent agent = this.owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.transform.position);

        // Get list of all friendly units within range
            // For each unit in range, add HealAmount to their CurrentHealth
    }
}
