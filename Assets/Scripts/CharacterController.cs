using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterController : MonoBehaviour
{
    protected enum TurnPhase { None, Begin, SelectCharacter, SelectMove, SelectAction, SelectTarget, Execution, End }

    public GameObject characterPrefab;

    protected List<Character> friendlies = new List<Character>();
    protected List<Character> enemies = new List<Character>();
    
    protected TurnPhase phase = TurnPhase.None;

    protected int subjectIndex = -1;
    protected Vector3 moveLocation = Vector3.zero;
    protected Action action = null;
    protected Target target = null;

    // Flow Control Booleans
    protected bool moveConfirmed = false;
    protected bool actionCanceled = false;
    protected bool actionConfirmed = false;

    public void StartTurn()
    {
        this.phase = TurnPhase.Begin;
    }

    protected void EndTurn()
    {
        this.subjectIndex = -1;
        this.moveLocation = Vector3.zero;
        this.action = null;
        this.target = null;
        this.moveConfirmed = false;
        this.actionCanceled = false;
        this.actionConfirmed = false;

        FindObjectOfType<GameManager>().FinishTurn();
    }

    public void CreateFriendlyCharacters(SpawnPoint spawnPoint)
    {
        for (int i = 0; i < 10; ++i)
        {
            GameObject newCharacter = Instantiate(characterPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            float randomX = Random.Range(-5, 5);
            float randomZ = Random.Range(-5, 5);
            newCharacter.transform.Translate(new Vector3(randomX, 0, randomZ));
            this.friendlies.Add(newCharacter.GetComponent<Character>());
        }
    }

    public void SetEnemy(CharacterController enemyController)
    {
        for (int i = 0; i < enemyController.friendlies.Count; ++i)
        {
            this.enemies.Add(enemyController.friendlies[i]);
        }
    }

    public void Update()
    {
        this.UpdateTurn();
    }

    protected void UpdateTurn()
    {
        switch (this.phase)
        {
            case TurnPhase.Begin:
                this.phase = TurnPhase.SelectCharacter;
                break;
            case TurnPhase.SelectCharacter:
                this.subjectIndex += 1;
                if (this.subjectIndex < this.friendlies.Count)
                {
                    this.phase = TurnPhase.SelectMove;
                }
                else
                {
                    this.phase = TurnPhase.End;
                }
                break;
            case TurnPhase.SelectMove:
                if (this.moveConfirmed)
                {
                    if (this.moveLocation != Vector3.zero)
                    {
                        this.friendlies[this.subjectIndex].MoveSelf(this.moveLocation);
                        this.phase = TurnPhase.SelectAction;
                    }
                    else
                    {
                        this.moveConfirmed = false;
                    }
                }
                else
                {
                    Vector3 location = GetLocationSelection();
                    if (location != Vector3.zero)
                    {
                        this.moveLocation = location;
                    }
                }
                break;
            case TurnPhase.SelectAction:
                Action action = GetAction();
                if (action != null)
                {
                    this.action = action;
                    this.phase = TurnPhase.SelectTarget;
                }
                break;
            case TurnPhase.SelectTarget:
                if (this.actionCanceled)
                {
                    this.actionCanceled = false;
                    this.actionConfirmed = false;
                    this.action = null;
                    this.phase = TurnPhase.SelectAction;
                }
                else if (this.actionConfirmed)
                {
                    this.phase = TurnPhase.Execution;
                }
                else
                {
                    Target target = GetTargetSelection();
                    if (target != null)
                    {
                        this.target = target;
                    }
                }
                break;
            case TurnPhase.Execution:
                switch (this.action.GetActionType())
                {
                    case Action.ActionType.Move:
                        this.friendlies[this.subjectIndex].MoveSelf(this.target.GetLocationTarget());
                        break;
                    case Action.ActionType.Attack:
                        // this.friendlies[this.subjectIndex].attack(this.target.GetCharacter());
                        break;
                }

                this.moveLocation = Vector3.zero;
                this.action = null;
                this.target = null;
                this.moveConfirmed = false;
                this.actionCanceled = false;
                this.actionConfirmed = false;

                this.phase = TurnPhase.SelectCharacter;
                break;
            case TurnPhase.End:
                this.EndTurn();
                this.phase = TurnPhase.None;
                break;
        }
    }

    protected void SetTargetType(Target target)
    {
        switch (this.action.GetActionType())
        {
            case Action.ActionType.Move:
                target.SetTargetType(Target.TargetType.Location);
                break;
            case Action.ActionType.Attack:
                target.SetTargetType(Target.TargetType.Enemy);  // selectedCharacter.GetAttackTargetType()
                break;
            case Action.ActionType.Ability:
                target.SetTargetType(Target.TargetType.Friendly);  // selectedCharacter.GetAbilityTargetType(actionAbilityNumber)
                break;
            case Action.ActionType.None:
                StartTurn();
                break;
        }
    }

    protected Target GetTargetSelection()
    {
        Target target = new Target();
        this.SetTargetType(target);

        Character character = null;
        Vector3 location = Vector3.zero;
        switch (target.GetTargetType())
        {
            case Target.TargetType.Self:
                if (this.friendlies[this.subjectIndex] != null)
                {
                    target.SetCharacterTarget(this.friendlies[this.subjectIndex]);
                    return target;
                }
                break;
            case Target.TargetType.Friendly:
                character = GetFriendlySelection();
                if (character != null)
                {
                    target.SetCharacterTarget(character);
                    return target;
                }
                break;
            case Target.TargetType.Enemy:
                character = GetEnemySelection();
                if (character != null)
                {
                    target.SetCharacterTarget(character);
                    return target;
                }
                break;
            case Target.TargetType.Location:
                location = GetLocationSelection();
                if (location != Vector3.zero)
                {
                    target.SetLocationTarget(location);
                    return target;
                }
                break;
        }

        return null;
    }

    public void ConfirmMove()
    {
        this.moveConfirmed = true;
    }

    public void CancelAction()
    {
        this.actionCanceled = true;
    }

    public void ConfirmAction()
    {
        this.actionConfirmed = true;
    }

    protected abstract Action GetAction();

    protected abstract Vector3 GetLocationSelection();

    protected abstract Character GetEnemySelection();

    protected abstract Character GetFriendlySelection();
}
