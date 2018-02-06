using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {
    protected HumanController hugh_man;
    protected Character current_char;
    public Sprite[] sprites;
    protected Image[] buttons;
    protected Slider[] sliders;
	// Use this for initialization
	void Start () {
        hugh_man = FindObjectOfType<HumanController>();
        buttons = this.gameObject.GetComponentsInChildren<Image>();
        sliders = this.gameObject.GetComponentsInChildren<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hugh_man.subjectIndex < 0){
            current_char = hugh_man.friendlies[hugh_man.subjectIndex];
            sliders[0].minValue = 0;
            sliders[0].maxValue = current_char.health;
        }
        else{
            this.gameObject.SetActive(false);
        }
    }
}
