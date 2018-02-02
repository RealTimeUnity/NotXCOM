using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterController : MonoBehaviour
{
    protected enum TurnPhase { None, Begin, SelectCharacter, SelectAbility, SelectTarget, Execution, End }

    public GameObject characterPrefab;

    public int maxMajorAbilities;
    public int maxMinorAbilities;
    protected int numMajorAbilities;
    protected int numMinorAbilities;

    protected List<Character> friendlies;
    protected List<Character> enemies;
    
    protected TurnPhase phase;
    protected int subjectIndex;

    protected Ability ability;
    protected Target target;
    protected bool abilityCanceled;
    protected bool abilityConfirmed;

    public void Start()
    {
        this.friendlies = new List<Character>();
        this.enemies = new List<Character>();
        this.phase = TurnPhase.None;
        this.subjectIndex = -1;
        this.ability = null;
        this.target = null;
        this.abilityCanceled = false;
        this.abilityConfirmed = false;
        this.numMajorAbilities = maxMajorAbilities;
        this.numMinorAbilities = maxMinorAbilities;
    }

    public void StartTurn()
    {
        this.phase = TurnPhase.Begin;
    }

    protected void EndTurn()
    {
        this.subjectIndex = -1;

        FindObjectOfType<GameManager>().FinishTurn();
    }

    public void CreateFriendlyCharacters(SpawnPoint spawnPoint)
    {
        this.friendlies = new List<Character>();
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

    protected bool IsAbilityExecutable(Ability ability)
    {
        bool result = false;
        Ability.AbilityType abilityType = ability.GetAbilityType();
        if (abilityType == Ability.AbilityType.Major &&
            this.numMajorAbilities > 0)
        {
            --this.numMajorAbilities;
            result = true;
        }
        if (abilityType == Ability.AbilityType.Minor &&
            this.numMinorAbilities > 0)
        {
            --this.numMinorAbilities;
            result = true;
        }

        return result;
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
                    this.phase = TurnPhase.SelectAbility;
                }
                else
                {
                    this.phase = TurnPhase.End;
                }
                break;
            case TurnPhase.SelectAbility:
                Ability ability = GetAbility();
                if (ability != null && this.IsAbilityExecutable(ability))
                {
                    this.ability = ability;
                    this.phase = TurnPhase.SelectTarget;
                }
                break;
            case TurnPhase.SelectTarget:
                if (this.abilityCanceled)
                {
                    this.ability = null;
                    this.target = null;
                    this.abilityCanceled = false;
                    this.abilityConfirmed = false;
                    this.phase = TurnPhase.SelectAbility;
                }
                else if (this.abilityConfirmed)
                {
                    if (this.target != null)
                    {
                        this.phase = TurnPhase.Execution;
                    }
                    else
                    {
                        this.abilityConfirmed = false;
                    }
                }
                else
                {
                    Target target = GetTargetSelection();
                    if (target != null && this.ability.IsTargetInRange(this.friendlies[this.subjectIndex], target))
                    {
                        this.target = target;
                    }
                }
                break;
            case TurnPhase.Execution:
                this.ability.Execute(this.target);
                if (this.numMajorAbilities + this.numMinorAbilities > 0)
                {
                    this.phase = TurnPhase.SelectAbility;
                }
                else
                {
                    this.numMajorAbilities = maxMajorAbilities;
                    this.numMinorAbilities = maxMinorAbilities;
                    this.phase = TurnPhase.SelectCharacter;
                }
                this.ability = null;
                this.target = null;
                this.abilityCanceled = false;
                this.abilityConfirmed = false;
                break;
            case TurnPhase.End:
                this.EndTurn();
                this.phase = TurnPhase.None;
                break;
        }
    }

    protected Target GetTargetSelection()
    {
        Target target = new Target();
        target.SetTargetType(this.ability.targetType);

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

    public void CancelAbility()
    {
        this.abilityCanceled = true;
    }

    public void ConfirmAbility()
    {
        this.abilityConfirmed = true;
    }

    protected abstract Ability GetAbility();

    protected abstract Vector3 GetLocationSelection();

    protected abstract Character GetEnemySelection();

    protected abstract Character GetFriendlySelection();
}
