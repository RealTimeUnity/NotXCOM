using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Singleton;

    [SerializeField]
    private PlayerController humanPlayer;
    [SerializeField]
    private PlayerController computerPlayer;

    [SerializeField]
    private Character[] characters;

    private PlayerType currentPlayer;

    public void Awake()
    {
        currentPlayer = PlayerType.Human;
        if (Singleton == null)
            Singleton = this;
        else
            DestroyImmediate(gameObject);
    }

    public void FinishTurn()
    {
        if (currentPlayer == PlayerType.Human)
        {
            currentPlayer = PlayerType.Computer;
            humanPlayer.OnTurnStart();
        }
        else
        {
            currentPlayer = PlayerType.Human;
            computerPlayer.OnTurnStart();
        }
    }
}
enum PlayerType
{
    Human,
    Computer
}
