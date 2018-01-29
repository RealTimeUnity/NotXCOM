using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {
    
    public Weapon primary_weapon;
    public float move_distance_max;
    public float move_distance_left;
    public float health;
    private bool can_action;//if move, attack, and abilities are used this is false
    public abilityParent ability1;
    public abilityParent ability2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void MoveSelf(Vector3 loc)//vector3 location where we want to go
    {
        //check to make sure its within max dist - dist left this turn
        GetComponent<NavMeshAgent>().SetDestination(loc);
        //set target for pathfinder
    }

    public void Attack(Target enemy)//a target object that we want to hit
    {
        //activte TakeDamage func for enemy
        //
    }

    public void UseAbility(int abilityNum)//int param passed 
    {

    }
}
