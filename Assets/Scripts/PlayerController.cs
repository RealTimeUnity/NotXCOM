using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour {

    protected enum TargetType { Location, Enemy, Friendly, Self };

    protected Character selectedCharacter = null;
    protected TargetType targetType = TargetType.Location;
    protected Character selectedCharacterTarget = null;
    protected Vector3 selectedLocationTarget = Vector3.zero;

    public void TurnStart()
    {
        this.selectedCharacter = null;
        this.targetType = TargetType.Location;
        this.selectedCharacterTarget = null;
        this.selectedLocationTarget = Vector3.zero;

        this.GetCharacterSelection();
        this.GetActionSelection();
    }

    public void OnTurnEnd()
    {
        GameManager.Singleton.FinishTurn();
    }

    public void AttackAction()
    {

    }

    public void MoveAction()
    {

    }

    public void AbilityAction(int abilityNumber)
    {

    }

    protected abstract void GetCharacterSelection();
    
    protected abstract void GetActionSelection();

    protected abstract void GetLocationSelection();

    protected abstract void GetEnemySelection();

    protected abstract void GetFriendlySelection();
}
