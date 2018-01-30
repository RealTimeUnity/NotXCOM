using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLoader : MonoBehaviour {
    public void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.SpawnWave();
    }
}
