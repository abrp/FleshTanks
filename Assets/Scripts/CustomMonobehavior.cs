//==============================================================================
// 
//==============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMonobehavior : MonoBehaviour {

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    //==============================================================================
    // Fields
    //==============================================================================
    private void Update () {
        switch (GameManager.instance.GetCurrentGameState)
        {
            case GameManager.GameState.Pre:
                PreLoop();
                break;
            case GameManager.GameState.Game:
                GameLoop();
                break;
            case GameManager.GameState.Pause:
                PauseLoop();
                break;
            case GameManager.GameState.End:
                EndLoop();
                break;
            default:
                PauseLoop();
                    break;
        }
    }

    //==============================================================================
    // Game Loops
    //==============================================================================

    protected virtual void PreLoop() {

    }

    protected virtual void GameLoop(){

    }

    protected virtual void PauseLoop(){

    }

    protected virtual void EndLoop()
    {

    }

}
