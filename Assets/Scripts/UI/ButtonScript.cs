using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonScript : MonoBehaviour {
    protected enum CombatPhase
    {
        None,
        ActionSelection,
        TargetSelection,
        ActionExecution
    }
    protected HumanController hugh_man;
    protected CharacterController char_man;
    protected Character current_char;
    public Image backGround;
    public Sprite[] sprites;
    protected Button[] buttons;
    protected Text[] text;
    protected Slider[] sliders;
    protected int prev_index = -1;
    protected int max_abilities = 6;
    protected int ability_count = 0;
    protected bool buttonClicked = false;
    protected CombatPhase curr_phase = CombatPhase.None;
    protected int current_button = -1;
	// Use this for initialization
	void Start () {
        hugh_man = FindObjectOfType<HumanController>();
        char_man = FindObjectOfType<CharacterController>();
        backGround = this.GetComponentInChildren<Image>();
        buttons = backGround.gameObject.GetComponentsInChildren<Button>();
        sliders = backGround.gameObject.GetComponentsInChildren<Slider>();
        text = backGround.gameObject.GetComponentsInChildren<Text>();
    }

    int buttonRollCall()
    {
        int ret = 0;
        for(int i = 0; i < ability_count; i++)
        {
            if (buttons[i].interactable)
            {
                ret++;
            }
        }
        return ret;
    }

    void cancel()
    {
        hugh_man.CancelAbility();
        buttonClicked = false;
        curr_phase = CombatPhase.None;
    }

    void updateInfo()
    {
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
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[max_abilities].onClick.RemoveAllListeners();
        buttons[max_abilities].onClick.AddListener(cancel);
        buttons[max_abilities].interactable = false;
        //filling in values
        for (int i = 0; i < current_char.abilities.Count; i++)
        {
            text[i + 1].text = current_char.abilities[i].abilityName;
            buttons[i].onClick.AddListener(delegate { buttonOnClick(i); });
            buttons[i].interactable = true;
            ability_count++;
        }
    }
	
    void buttonOnClick(int buttonIndex)
    {
        if (!buttonClicked)
        {
            current_button = buttonIndex;
            buttonClicked = true;
            hugh_man.SelectAbility(text[current_button].text);
            curr_phase = CombatPhase.TargetSelection;
        }
        else
        {
            buttonClicked = false;
            hugh_man.ConfirmAbility();
            curr_phase = CombatPhase.ActionExecution;
        }
    }

	// Update is called once per frame
	void Update () {
        switch (curr_phase)
        {
            case CombatPhase.None:
                if (hugh_man.subjectIndex != -1)
                {
                    this.backGround.gameObject.SetActive(true);
                    if (hugh_man.subjectIndex != prev_index)
                    {
                        updateInfo();
                        prev_index = hugh_man.subjectIndex;
                        curr_phase = CombatPhase.ActionSelection;
                    }
                }
                else
                {
                    this.backGround.gameObject.SetActive(false);
                }
                break;
            case CombatPhase.ActionSelection:
                if(hugh_man.subjectIndex == -1 || hugh_man.subjectIndex != prev_index)
                {
                    curr_phase = CombatPhase.None;
                }
                break;
            case CombatPhase.TargetSelection:
                if(buttonRollCall() != 1)
                {
                    for(int i = 0; i < ability_count; i++)
                    {
                        buttons[i].interactable = false;
                    }
                    buttons[current_button].interactable = true;
                    if (!buttons[max_abilities].interactable)
                    {
                        buttons[max_abilities].interactable = true;
                    }
                }
                break;
            case CombatPhase.ActionExecution:
                //wait for an undetermined ammount of time
                buttons[max_abilities].interactable = false;
                curr_phase = CombatPhase.None;
                break;
        }
    }
}
