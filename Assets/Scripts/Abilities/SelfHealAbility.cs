﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelfHealAbility : Ability
{
    public float healAmount=0.75F;

    public override void Execute(Target target)
    {
        base.Execute(target);
        Character destination = target.GetCharacterTarget();

        NavMeshAgent agent = this.owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.transform.position);

        this.owner.currentHealth += healAmount;
        if (this.owner.currentHealth > this.owner.MaxHealth)
            {
            this.owner.currentHealth = this.owner.MaxHealth;
        }
    }
}
