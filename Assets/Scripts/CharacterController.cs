using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour {

    protected enum TargetType { None, Location, Enemy, Friendly, Self };
    protected enum ActionType { None, Move, Attack, Ability }

    public int startingCharacters = 0;

    protected List<Character> friendlies = new List<Character>();
    protected List<Character> enemies = new List<Character>();

    // Subject performs Action to Target
    // Subject Attributes
    protected Character subject = null;

    // Action Attributes
    protected ActionType actionType = ActionType.None;
    protected int actionAbilityNumber = 0;

    // Target Attributes
    protected TargetType targetType = TargetType.None;
    protected Character targetCharacter = null;
    protected Vector3 targetLocation = Vector3.zero;

    public void StartTurn()
    {
        this.subject = null;
        this.actionType = ActionType.None;
        this.actionAbilityNumber = 0;
        this.targetType = TargetType.None;
        this.targetCharacter = null;
        this.targetLocation = Vector3.zero;

        this.GetCharacterSelection();
        this.GetActionType();
        this.GetTargetSelection();
    }

    protected void EndTurn()
    {
        // Change this to not a singleton reference
        GameManager.Singleton.FinishTurn();
    }

    protected void CreateFriendlyCharacters()
    {
        for (int i = 0; i < this.startingCharacters; ++i)
        {
            this.friendlies.Add(new Character());
        }
    }

    protected void SetEnemy(CharacterController enemyController)
    {
        for (int i = 0; i < enemyController.friendlies.Count; ++i)
        {
            this.enemies.Add(enemyController.friendlies[i]);
        }
    }

    protected void GetTargetSelection()
    {
        switch (this.actionType)
        {
            case ActionType.Move:
                this.targetType = TargetType.Location;
                break;
            case ActionType.Attack:
                this.targetType = TargetType.Enemy;  // selectedCharacter.GetAttackTargetType()
                break;
            case ActionType.Ability:
                this.targetType = TargetType.Friendly;  // selectedCharacter.GetAbilityTargetType(actionAbilityNumber)
                break;
            case ActionType.None:
                StartTurn();
                break;
        }

        switch (this.targetType)
        {
            case TargetType.Self:
                this.targetCharacter = this.subject;
                break;
            case TargetType.Friendly:
                GetFriendlySelection();
                break;
            case TargetType.Enemy:
                GetEnemySelection();
                break;
            case TargetType.Location:
                GetLocationSelection();
                break;
            case TargetType.None:
                StartTurn();
                break;
        }
    }

    protected abstract void GetCharacterSelection();

    protected abstract void GetActionType();

    protected abstract void GetLocationSelection();

    protected abstract void GetEnemySelection();

    protected abstract void GetFriendlySelection();
}
