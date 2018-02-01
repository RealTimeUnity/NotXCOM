using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action
{
    public enum ActionType { None, Move, Attack, Ability }

    private ActionType type = ActionType.None;
    private int abilityNumber = 0;

    public ActionType GetActionType()
    {
        return this.type;
    }

    public void SetActionType(ActionType type)
    {
        this.type = type;
    }

    public int GetAbilityNumber()
    {
        if (this.type == ActionType.Ability)
        {
            return this.abilityNumber;
        }

        return -1;
    }

    public void SetAbilityNumber(int abilityNumber)
    {
        if (this.type == ActionType.Ability)
        {
            this.abilityNumber = abilityNumber;
        }
    }
}