using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HumanController : CharacterController
{
    public GameObject ActionConfirmUI;
    public GameObject MoveConfirmUI;
    public GameObject CombatUI;
    public GameObject locationPointer;

    protected Action selectedAction = null;

    public void SelectAttackAction()
    {
        Action action = new Action();
        action.SetActionType(Action.ActionType.Attack);
        this.selectedAction = action;
    }

    public void SelectMoveAction()
    {
        Action action = new Action();
        action.SetActionType(Action.ActionType.Move);
        this.selectedAction = action;
    }

    public void SelectAbilityOneAction()
    {
        Action action = new Action();
        action.SetActionType(Action.ActionType.Ability);
        action.SetAbilityNumber(0);
        this.selectedAction = action;
    }

    public void SelectAbilityTwoAction()
    {
        Action action = new Action();
        action.SetActionType(Action.ActionType.Ability);
        action.SetAbilityNumber(1);
        this.selectedAction = action;
    }

    protected override Action GetAction()
    {
        if (this.selectedAction != null)
        {
            Action result = this.selectedAction;
            this.selectedAction = null;
            return result;
        }

        return null;
    }

    protected override Vector3 GetLocationSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return Vector3.zero;
            }
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                NavMeshHit nhit;
                if (NavMesh.SamplePosition(hit.point, out nhit, 10.0f, NavMesh.AllAreas))
                {
                    return nhit.position;
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
    
    protected Character GetCharacterClickedOn()
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

    new public void Update()
    {
        this.UpdateTurn();
        this.UpdateVisuals();
        this.UpdateActiveUI();
    }

    protected void UpdateVisuals()
    {
        switch (this.phase)
        {
            case TurnPhase.Begin:
                break;
            case TurnPhase.SelectCharacter:
            case TurnPhase.SelectMove:
            case TurnPhase.SelectAction:
            case TurnPhase.SelectTarget:
            case TurnPhase.Execution:
                for (int i = 0; i < this.friendlies.Count; ++i)
                {
                    this.friendlies[i].GetComponent<MeshRenderer>().material.SetInt("_Highlighted", 0);
                }

                if (this.subjectIndex < this.friendlies.Count && this.subjectIndex >= 0)
                {
                    this.friendlies[this.subjectIndex].GetComponent<MeshRenderer>().material.SetInt("_Highlighted", 1);
                }
                break;
            case TurnPhase.End:
                break;
        }
    }

    protected void UpdateActiveUI()
    {
        switch (this.phase)
        {
            case TurnPhase.None:
            case TurnPhase.Begin:
            case TurnPhase.SelectCharacter:
            case TurnPhase.Execution:
            case TurnPhase.End:
                this.ActionConfirmUI.SetActive(false);
                this.MoveConfirmUI.SetActive(false);
                this.CombatUI.SetActive(false);
                break;
            case TurnPhase.SelectMove:
                this.ActionConfirmUI.SetActive(false);
                this.MoveConfirmUI.SetActive(true);
                this.CombatUI.SetActive(false);
                break;
            case TurnPhase.SelectAction:
                this.ActionConfirmUI.SetActive(false);
                this.MoveConfirmUI.SetActive(false);
                this.CombatUI.SetActive(true);
                break;
            case TurnPhase.SelectTarget:
                this.ActionConfirmUI.SetActive(true);
                this.MoveConfirmUI.SetActive(false);
                this.CombatUI.SetActive(false);
                break;
        }
    }
}
