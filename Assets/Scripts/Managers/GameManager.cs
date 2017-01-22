using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    public static GameManager instance = null;

    public enum GameState {Pre, Game, End, Pause};

    [SerializeField]
    private GameState m_CurrentGameState = GameState.Pre;

    [SerializeField]
    private Text m_WinScreen;

    [SerializeField]
    private GameObject m_WinObject;

    //==============================================================================
    // Delegates
    //==============================================================================

    public delegate void GameManagerEventHandler();
    public GameManagerEventHandler onStateChangeCallBack;

    //==============================================================================
    // Properties
    //==============================================================================

    public GameState GetCurrentGameState {
        get { return m_CurrentGameState;  }
    }

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    //==============================================================================
    public void Awake()
    {
        SetupSingleton();
    }

    private void Update()
    {

        if (m_CurrentGameState == GameState.End) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    //==============================================================================
    private void SetupSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    //==============================================================================
    public void SetGameState(GameState newGameState) {
        m_CurrentGameState = newGameState;

        // game state changed
        if (onStateChangeCallBack != null)
        {
            onStateChangeCallBack();
        }
    }

    public void CheckIfGameWon() {
        Player[] players = FindObjectsOfType<Player>();

        Debug.Log("CHECK IF GAME WON");

        if (players.Length >= 2) {

            int numberOfPlayers = players.Length;

            Debug.Log("numberOfActivePlayer: " + numberOfPlayers);

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].IsDonePlaying) {
                    numberOfPlayers--;
                }
            }

            if (numberOfPlayers == 1) {
                m_WinObject.SetActive(true);
                m_WinScreen.gameObject.SetActive(true);
                int winner = GetWinner(players);
                m_WinScreen.text = "Player " + winner + " has won the game";
                SetGameState(GameState.End);
            }
        }
    }

    private int GetWinner(Player[] players) {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].IsDonePlaying == false) {
                return players[i].PlayerNumber;
            }
        }

        return 0;
    }
}
