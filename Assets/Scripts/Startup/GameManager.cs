using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private CharacterController humanPlayer;
    [SerializeField]
    private CharacterController computerPlayer;

    [SerializeField]
    private Character[] characters;

    private PlayerType currentPlayer;
    
    public void Start()
    {
        currentPlayer = PlayerType.Computer;
        computerPlayer.StartTurn();
    }

    public void SpawnWave(WaveLoader waveLoader)
    {
        humanPlayer.CreateFriendlyCharacters(waveLoader.GetRandomSpawnPoint());
        computerPlayer.CreateFriendlyCharacters(waveLoader.GetRandomSpawnPoint());

        humanPlayer.SetEnemy(computerPlayer);
        computerPlayer.SetEnemy(humanPlayer);
    }

    public void FinishTurn()
    {
        if (currentPlayer == PlayerType.Human)
        {
            currentPlayer = PlayerType.Computer;
            humanPlayer.StartTurn();
        }
        else
        {
            currentPlayer = PlayerType.Human;
            computerPlayer.StartTurn();
        }
    }
}
enum PlayerType
{
    Human,
    Computer
}
