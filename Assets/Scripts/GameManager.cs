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
        currentPlayer = PlayerType.Human;
        humanPlayer.StartTurn();
    }

    public void SpawnWave()
    {
        humanPlayer.CreateFriendlyCharacters();
        computerPlayer.CreateFriendlyCharacters();

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
