using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {

    public Weapon primary_weapon;

    public float health;

    public int maxMajorAbilities;
    public int maxMinorAbilities;
    protected int numMajorAbilities;
    protected int numMinorAbilities;

    public List<Ability> abilityPrefabs;

    [HideInInspector]
    public List<Ability> abilities;

    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        this.abilities = new List<Ability>();
        for (int i = 0; i < this.abilityPrefabs.Count; ++i)
        {
            Ability ability = Instantiate(abilityPrefabs[i], this.gameObject.transform);
            ability.Initialize(this);
            this.abilities.Add(ability);
        }
        this.ResetTurn();

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        anim.SetFloat("Forward", agent.velocity.magnitude / 3.0f);                
    }

    public bool HasAbility(string abilityName)
    {
        bool result = false;
        for (int i = 0; i < this.abilities.Count; i++)
        {
            if (this.abilities[i].abilityName == abilityName)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    public Ability GetAbility(string abilityName)
    {
        Ability result = null;
        for (int i = 0; i < this.abilities.Count; i++)
        {
            if (this.abilities[i].abilityName == abilityName)
            {
                result = this.abilities[i];
            }
        }

        return result;
    }

    public bool IsAbilityExecutable(string abilityName)
    {
        Ability ability = this.GetAbility(abilityName);
        bool result = false;
        Ability.AbilityType abilityType = ability.GetAbilityType();

        if (ability.uses > 0)
        {
            if (abilityType == Ability.AbilityType.Major &&
            this.numMajorAbilities > 0)
            {
                result = true;
            }
            if (abilityType == Ability.AbilityType.Minor &&
                this.numMinorAbilities > 0)
            {
                result = true;
            }
        }

        return result;
    }

    public bool HasMoreAbilities()
    {
        bool result = false;

        for (int i = 0; i < this.abilities.Count; ++i)
        {
            if (this.abilities[i].GetAbilityType() == Ability.AbilityType.Major)
            {
                if (this.abilities[i].uses > 0 && this.numMajorAbilities > 0)
                {
                    result = true;
                    break;
                }
            }
            else
            {
                if (this.abilities[i].uses > 0 && this.numMinorAbilities > 0)
                {
                    result = true;
                    break;
                }
            }
        }

        return result;
    }

    public void ResetTurn()
    {
        for (int i = 0; i < this.abilities.Count; ++i)
        {
            this.abilities[i].ResetCount();
        }

        this.numMajorAbilities = maxMajorAbilities;
        this.numMinorAbilities = maxMinorAbilities;
    }

    public void ExecuteAbility(string abilityName, Target target)
    {
        Ability ability = this.GetAbility(abilityName);
        Ability.AbilityType abilityType = ability.GetAbilityType();
        if (abilityType == Ability.AbilityType.Major)
        {
            --this.numMajorAbilities;
        }
        if (abilityType == Ability.AbilityType.Minor)
        {
            --this.numMinorAbilities;
        }

        ability.Execute(target);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
