using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability : MonoBehaviour
{
    public enum AbilityType { None, Major, Minor, Passive }

    public Target.TargetType targetType;
    public AbilityType type = AbilityType.None;
    public int range;

    protected Character owner;

    public AbilityType GetAbilityType()
    {
        return this.type;
    }

    public void Initialize(Character owner)
    {
        this.owner = owner;
    }

    public bool IsTargetInRange(Character startingPoint, Target target)
    {
        Vector3 destination = Vector3.zero;
        switch (this.targetType)
        {
            case Target.TargetType.Location:
                destination = target.GetLocationTarget();
                break;
            case Target.TargetType.Enemy:
            case Target.TargetType.Friendly:
                destination = target.GetCharacterTarget().transform.position;
                break;
            case Target.TargetType.Self:
                destination = startingPoint.transform.position;
                break;
        }

        float distance = Vector3.Distance(startingPoint.transform.position, destination);
        bool result = distance <= this.range;
        return result;
    }

    public abstract void Execute(Target target);
}