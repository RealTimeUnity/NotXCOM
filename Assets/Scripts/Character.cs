using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public PlayerController owner;
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

    public void takeDamage(int damage)
    {
        health -= damage;
    }
    void dealDamage(Character enemy)//this will eventually take an instance of the thing we are shooting at
    {
        //pass weapons damage in to the take damage function of the thing we are shooting at
       // enemy.takeDamage(primary_weapon.Attack(enemy));
    }
    

}
