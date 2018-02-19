using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HumanController : CharacterController
{
    public GameObject locationIndicator;
    public GameObject rangeIndicator;
    public GameObject characterIndicator;

    protected string selectedAbilityName = null;

    public void SelectAbility(string name)
    {
        if (this.friendlies[this.subjectIndex].HasAbility(name))
        {
            this.selectedAbilityName = name;
        }
    }

    protected override string GetAbilityName()
    {
        string result = null;
        if (this.selectedAbilityName != null)
        {
            result = this.selectedAbilityName;
            this.selectedAbilityName = null;
        }

        return result;
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
    }

    protected void UpdateVisuals()
    {
        // Subject Highlighting
        if (this.subjectIndex != -1)
        {
            for (int i = 0; i < this.friendlies.Count; ++i)
            {
                MeshRenderer mr = friendlies[i].GetComponent<MeshRenderer>();
                if (mr == null)
                    friendlies[i].GetComponentInChildren<SkinnedMeshRenderer>().material.SetInt("_Highlighted", 0);
                else
                    mr.material.SetInt("_Highlighted", 0);
            }

            if (this.subjectIndex < this.friendlies.Count && this.subjectIndex >= 0)
            {
                MeshRenderer mr = friendlies[subjectIndex].GetComponent<MeshRenderer>();
                if (mr == null)
                {
                    friendlies[subjectIndex].GetComponentInChildren<SkinnedMeshRenderer>().material.SetInt("_Highlighted", 0);
                    friendlies[subjectIndex].GetComponentInChildren<SkinnedMeshRenderer>().material.SetInt("_Highlighted", 1);
                    friendlies[subjectIndex].GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("_OutlineColor", new Color(0, 1, 0));
                }
                else
                {
                    mr.material.SetInt("_Highlighted", 0);
                    mr.material.SetInt("_Highlighted", 1);
                    mr.material.SetColor("_OutlineColor", new Color(0, 1, 0));
                }
            }
        }

        // Range Indicator
        if (this.abilityName != null)
        {

            NavMeshHit nhit;
            NavMesh.SamplePosition(this.friendlies[this.subjectIndex].transform.position, out nhit, 10.0f, NavMesh.AllAreas);

            this.rangeIndicator.transform.position = nhit.position;
            this.rangeIndicator.GetComponent<RangeIndicator>().Initialize(this.friendlies[this.subjectIndex].GetAbility(this.abilityName).range);
        }
        else
        {
            this.rangeIndicator.SetActive(false);
        }

        // Target Indicator
        if (this.target != null)
        {
            if (this.target.GetTargetType() == Target.TargetType.Location)
            {
                this.locationIndicator.transform.position = this.target.GetLocationTarget();
                this.locationIndicator.SetActive(true);
                this.characterIndicator.SetActive(false);
            }
            else if (this.target.GetTargetType() != Target.TargetType.None)
            {
                this.characterIndicator.transform.position = this.target.GetCharacterTarget().transform.position;
                this.characterIndicator.SetActive(true);
                this.locationIndicator.SetActive(false);
            }
        }
        else
        {
            this.locationIndicator.SetActive(false);
            this.characterIndicator.SetActive(false);
        }
    }
}
