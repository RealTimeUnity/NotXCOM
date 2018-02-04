using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    protected float Range;
    protected int Damage;

    void Start()//Initializes stats
    { }

	public int Get_Damage(){
		return Damage;
	}

	public float Get_Range(){
		return Range;
	}

	public int Target(Character c){
		int accuracy = 0;
		float dis = Vector3.Distance(this.transform.position, c.transform.position);
		if(dis > Range)
		{
			if(dis < (2 * Range))
			{
				accuracy = (int)(((dis % Range) / Range) * 100);
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
