using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Ability {

    protected int Damage;

    void Start()//Initializes stats
    { }

	public int Get_Damage(){
		return Damage;
	}

	public float Get_Range(){
		return range;
	}

	public int Target(Character startingPoint, Target target)
    {
        int accuracy = 0;
		if(!IsTargetInRange(startingPoint, target))
		{
            float distance = Vector3.Distance(startingPoint.transform.position, target.GetCharacterTarget().transform.position);
            if (distance < (2 * range))
			{
				accuracy = (int)(((distance % range) / range) * 100);
			}
		}
		else {
			accuracy = 100;
		}
		return accuracy;
	}

    // Attack Function
	public int Attack (int accuracy) {
		int dam = 0;
		System.Random rand = new System.Random ();
		int num = rand.Next (0, 100);
		if(num < accuracy){
            dam = Damage;
        }
        return dam;
	}
}
