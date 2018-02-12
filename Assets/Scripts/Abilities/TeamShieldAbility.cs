using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamShieldAbility : Ability
{

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < owner.owner.friendlies.Count; i++)
        {
            float distance = Mathf.Sqrt(Mathf.Pow((owner.owner.friendlies[i].gameObject.transform.position.x - this.owner.gameObject.transform.position.x), 2) +
                    Mathf.Pow((owner.owner.friendlies[i].gameObject.transform.position.z - this.owner.gameObject.transform.position.z), 2));
            if (distance < 30)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
