using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : CharacterController
{
    protected Action selectedAction = null;

    protected void SelectAttackAction()
    {
        Action action = new Action();
        action.setType(Action.ActionType.Attack);
        this.selectedAction = action;
    }

    protected void SelectMoveAction()
    {
        Action action = new Action();
        action.setType(Action.ActionType.Attack);
        this.selectedAction = action;
    }

    protected void SelectAbilityOneAction()
    {
        Action action = new Action();
        action.setType(Action.ActionType.Attack);
        action.setAbilityNumber(0);
        this.selectedAction = action;
    }

    protected void SelectAbilityTwoAction()
    {
        Action action = new Action();
        action.setType(Action.ActionType.Attack);
        action.setAbilityNumber(1);
        this.selectedAction = action;
    }

    protected override Action GetAction()
    {
        if (this.selectedAction != null)
        {
            return this.selectedAction;
        }

        return null;
    }

    protected override Vector3 GetLocationSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.gameObject.tag == "Floor")
                {
                    return hit.point;
                }
            }
        }

        return Vector3.zero;
    }

    protected override Character GetEnemySelection()
    {
        Character character = GetCharacterClickedOn();
        if (this.enemies.Contains(character))
        {
            return character;
        }

        return null;
    }

    protected override Character GetFriendlySelection()
    {
        Character character = GetCharacterClickedOn();
        if (this.friendlies.Contains(character))
        {
            return character;
        }

        return null;
    }
    
    private Character GetCharacterClickedOn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Character selectedChar = hit.collider.GetComponent<Character>();
                if (selectedChar != null)
                {
                    return selectedChar;
                }
            }
        }

        return null;
    }
}
