using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float Range;
    public int Damage;
    public int Speed; //Eventually will attack multiple times, depending on weapon speed

    void Start()//Initializes stats
    {
        /*
        if (type == weaponType.Pistol)
        {
            Range = 25;
            Damage = 5;
        }
        else if (type == weaponType.Shotgun)
        {
            Range = 25;
            Damage = 40;
        }
        else if (type == weaponType.Rifle)
        {
            Range = 50;
            Damage = 15;
        }
        else if (type == weaponType.Sniper)
        {
            Range = 100;
            Damage = 40;
        }*/
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
