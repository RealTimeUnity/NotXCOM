using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLoader : MonoBehaviour {

    public List<SpawnPoint> spawnPoints;

    public void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.SpawnWave();
    }

    public SpawnPoint GetRandomSpawnPoint()
    {
        int index = Random.Range(0, this.spawnPoints.Count - 1);
        while (!this.spawnPoints[index].IsOccupied)
        {
            index = Random.Range(0, this.spawnPoints.Count - 1);
        }

        return this.spawnPoints[index];
    }
}
