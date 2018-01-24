using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : CharacterController
{
    public Shader shader1;


    protected override void GetCharacterSelection()
    {

    }

    protected override void GetActionType()
    {

    }

    protected override void GetLocationSelection()
    {

    }

    protected override void GetEnemySelection()
    {

    }

    protected override void GetFriendlySelection()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EndTurn();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(lastSelectedChar == null)
                SelectCharacter();
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    NavMeshHit nhit;
                    if (NavMesh.SamplePosition(hit.point, out nhit, 10.0f, NavMesh.AllAreas))
                    {
                        lastSelectedChar.GetComponent<NavMeshAgent>().SetDestination(nhit.position);
                        lastSelectedChar.GetComponent<MeshRenderer>().material.SetInt("_Highlighted", 0);
                        lastSelectedChar = null;
                    }
                }
            }
        }
    }

    public void cancelAction()
    {
        this.actionCanceled = true;
    }

    public void SelectedCharacterAttack()
    {
        // this is to be called when a gui button is pressed for the selected character to attack
        // display radius
        // Track mouse with waypoint? (maybe not here) (maybe do this all the time)
        // turn off gui menu and don't allow it to turn back on unless the player clicks a cancel button
        lastSelectedChar = null;
        IEnumerator coroutine = WaitForTargetSelection();
        StartCoroutine(coroutine);
    }

    public void SelectedCharacterMove()
    {
        // this is to be called when a gui button is pressed to move the selected character
        // display radius
        // Track mouse with waypoint? (maybe not here) (maybe do this all the time)
        // turn off gui menu and don't allow it to turn back on unless the player clicks a cancel button
        lastSelectedChar = null;
        IEnumerator coroutine = WaitForMovePosition();
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitForTargetSelection()
    {
        while (true)
        {
            Character selectedTarget = GetCharacterClickedOn();
            if (selectedTarget != null && selectedTarget.owner != this)
            {
                // lastSelectedChar.attack(selectedTarget)
                yield break;
            }

            if (actionCanceled)
            {
                actionCanceled = false;
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator WaitForMovePosition()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.gameObject.tag == "Floor")
                    {
                        // lastSelectedChar.move(hit.point)
                        yield break;
                    }
                }
            }

            if (actionCanceled)
            {
                actionCanceled = false;
                yield break;
            }
            yield return null;
        }
    }

    private void SelectCharacter()
    {
        Character selectedCharacter = GetCharacterClickedOn();
        if (lastSelectedChar != null)
        {
            lastSelectedChar.GetComponent<MeshRenderer>().material.SetInt("_Highlighted", 0);
        }
        lastSelectedChar = selectedCharacter;
    }

    private Character GetCharacterClickedOn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * 200, Color.red, 1.0f, true);
            Character selectedChar = hit.collider.GetComponent<Character>();
            if(selectedChar != null && selectedChar.owner == this)
            {
                // add outline, particle shiz, etc...
                selectedChar.GetComponent<MeshRenderer>().material.SetInt("_Highlighted", 1);
                return selectedChar;
            }
            else
            {
                //deal with it later, not selecting a cube
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 200, Color.blue, 1.0f, true);
        }

        return null;
    }
}
