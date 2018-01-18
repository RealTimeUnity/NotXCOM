using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : PlayerController
{

    public Character enemy1;
    public Character enemy2;
    public Character enemy3;
    public Character p1;
    public Character p2;
    public Character p3;
    
    public override void OnTurnStart()
    {
    }
    public void Start()
    {
    }
    public void Update()
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
    }
}
