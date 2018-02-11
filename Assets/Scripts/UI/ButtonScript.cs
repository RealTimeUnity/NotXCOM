﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    protected HumanController hugh_man;
    protected Character current_char;
    public Image backGround;
    public Sprite[] sprites;
    protected Button[] buttons;
    protected Text[] text;
    protected Slider[] sliders;
    protected int prev_index = -1;
    protected int max_abilities = 6;
    protected int ability_count = 0;
	// Use this for initialization
	void Start () {
        hugh_man = FindObjectOfType<HumanController>();
        backGround = this.GetComponentInChildren<Image>();
        buttons = backGround.gameObject.GetComponentsInChildren<Button>();
        sliders = backGround.gameObject.GetComponentsInChildren<Slider>();
        text = backGround.gameObject.GetComponentsInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hugh_man.subjectIndex != -1) {
            
            this.backGround.gameObject.SetActive(true);
            if (hugh_man.subjectIndex != prev_index)
            {
                prev_index = hugh_man.subjectIndex;
                current_char = hugh_man.friendlies[hugh_man.subjectIndex];
                //setting name
                text[0].text = current_char.Name;
                //setting health slider
                sliders[0].minValue = 0;
                sliders[0].maxValue = current_char.MaxHealth;
                sliders[0].value = current_char.currentHealth;
                //setting ability bar to empty

                for (int i = 1; i < max_abilities; i++)
                {
                    text[i + 1].text = "";
                    buttons[i].interactable = false;
                    ability_count++;
                }
                //filling in values
                for (int i = 0; i < current_char.abilities.Count; i++)
                {
                    text[i + 1].text = current_char.abilities[i].abilityName;
                    buttons[i].interactable = true;
                    ability_count++;
                }
            }
        }
        else
        {
            this.backGround.gameObject.SetActive(false);
        }
    }
}
