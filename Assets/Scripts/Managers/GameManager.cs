using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    public static GameManager instance = null;

    public enum GameState {Pre, Game, End, Pause};

    private GameState m_CurrentGameState = GameState.Pre;

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
    }
}
