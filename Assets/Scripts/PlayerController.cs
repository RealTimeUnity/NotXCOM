using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour {

    protected Character selectedCharacter;
    protected string selectedAction;
    protected Character selectedCharacterTarget;
    protected RaycastHit selectedLocationTarget;


    public void OnTurnStart()
    {
        this.ExecuteTurn();
    }

    public void OnTurnEnd()
    {
        GameManager.Singleton.FinishTurn();
    }

    public void CancelTurn()
    {

    }

    public void ExecuteTurn()
    {

    }

    protected void GetTargetSelection()
    {
        // check that selectedCharacter is not NULL
        switch (selectedAction)
        {
            case "MOVE":
                this.GetLocationSelection();
                break;
            case "ATTACK":
                this.GetEnemySelection();
                break;
            case "ABILITY":
                //abilityParent ability = selectedCharacter.getAbilityOne()
                string abilityType = "FRIENDLY";  // ability.getType();
                switch (abilityType)
                {
                    case "FRIENDLY":
                        GetFriendlySelection();
                        break;
                    case "ENEMY":
                        GetEnemySelection();
                        break;
                    case "LOCATION":
                        GetLocationSelection();
                        break;
                    case "SELF":
                        this.selectedCharacterTarget = selectedCharacter;
                        break;
                }
                break;
        }
    }
    
    protected abstract void GetActionSelection();

    protected abstract void GetLocationSelection();

    protected abstract void GetEnemySelection();

    protected abstract void GetFriendlySelection();
}
