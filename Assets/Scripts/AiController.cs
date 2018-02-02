using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : CharacterController
{
    protected override Ability GetAbility()
    {
        return null;
    }

    protected override Vector3 GetLocationSelection()
    {
        return Vector3.zero;
    }

    protected override Character GetEnemySelection()
    {
        return null;
    }

    protected override Character GetFriendlySelection()
    {
        return null;
    }
    protected int magicalHurtFormula(Character actor, Character victim)
    {
        /*magical formula used to determine the value of shooting at each target*/
        int hurtValue = 0;
        int accuracy=actor.primary_weapon.Target(victim);
        int damage = actor.primary_weapon.Get_Damage();
        hurtValue = accuracy * damage;
        if (damage > victim.health)
        {
            hurtValue=hurtValue*2+10;
        }
        return (hurtValue);
    }
    protected Character selectVictim(Character actor)
    {
        /*this code will select the best target for each enemy to shoot at
         * If no target is worth shooting at, it will move to the closest enemy
         * defaults to enemy[0] for convenience*/

        Character tempVictim=enemies[0];
        int hurtocity = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            int tempVal = magicalHurtFormula(actor, enemies[i]);
            if (tempVal > hurtocity)
            {
                hurtocity = tempVal;
                tempVictim = enemies[i];
            }
        }
        if (hurtocity < actor.primary_weapon.Get_Damage() * .20)
        {
            /*movement target selection*/
            //int speedLimit = (int)actor.move_distance_max;
            int distance = 100000;
            Vector3 dir = Vector3.down;
            Vector3 temp = actor.GetComponent<Transform>().position;
            for (int i = 0; i < enemies.Count; i++)
            {
                int tempRange = (int)Vector3.Distance(temp, enemies[i].GetComponent<Transform>().position);
                if (tempRange < distance)
                {
                    distance = tempRange;
                    dir = enemies[i].GetComponent<Transform>().position;
                    tempVictim = enemies[i];
                }
            }
        }
        return (tempVictim);

    }
    protected bool attackingOrMoving(Character actor)
    {
        /*true is attack, false is moving*/
        int hurtocity = 0;
        for (int i = 0; i < enemies.Count; i++)
        {
            int tempVal = magicalHurtFormula(actor, enemies[i]);
            if (tempVal > hurtocity)
            {
                hurtocity = tempVal;
            }
        }
        if (hurtocity < actor.primary_weapon.Get_Damage() * .20)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }
    /*public void Update()
    {
        int range = 10000;
        int tempRange = range;
        Vector3 dir = Vector3.down;
        Vector3 temp;
        temp = p1.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy1.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        temp = p2.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy1.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        temp = p3.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy1.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        enemy1.GetComponent<NavMeshAgent>().SetDestination(dir);

        temp = p1.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy2.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        temp = p2.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy2.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        temp = p3.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy2.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        enemy2.GetComponent<NavMeshAgent>().SetDestination(dir);

        temp = p1.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy3.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        temp = p2.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy3.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        temp = p3.GetComponent<Transform>().position;
        tempRange = (int)Vector3.Distance(temp, enemy3.GetComponent<Transform>().position);
        if (tempRange < range)
        {
            range = tempRange;
            dir = temp;
        }
        enemy3.GetComponent<NavMeshAgent>().SetDestination(dir);
    }*/
}
