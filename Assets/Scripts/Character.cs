using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {

    public Weapon primary_weapon;

    public float health;

    public List<Ability> abilities;
    
    void Start ()
    {

    }
	
	void Update ()
    {
		
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
