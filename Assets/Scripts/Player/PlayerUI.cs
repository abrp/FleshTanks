using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    [SerializeField]
    private Text m_Lives;
    [SerializeField]
    private Text m_Score;

    public void UpdateLives(int lives) {
        m_Lives.text = lives.ToString();
    }

    public void UpdateScore(int score) {
        m_Score.text = score.ToString();
    }
    

}
