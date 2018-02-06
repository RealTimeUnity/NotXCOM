using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfShield : Ability {
    public bool ShieldActive=false;
    public float MaxShieldHealth = 100;
    public float CurrentShieldHealth = 0;
    public override void Execute(Target target)
    {
        base.Execute(target);
        CurrentShieldHealth = 100;

    }
    public void Update()
    {

        if (CurrentShieldHealth==0)
        {
            ShieldActive = false;
        }
        //check if enemy turn over then set CurrentShieldHealth to 0
        if (/*owner.owner.*/true)
        {

        }
    }
}
