using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerManager : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    public static PlayerManager instance = null;

    private int m_MinPlayers = 2;

    private bool[] m_PlayerInPlay = new bool[4];

    [SerializeField]
    private Player m_PlayersPrefab; 

    [SerializeField]
    private SpawnPoint[] m_SpawnPoints;

    //==============================================================================
    public void Awake()
    {
        SetupSingleton();
    }


    //==============================================================================
    // Singleton
    //==============================================================================

    //==============================================================================

    private void SetupSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    //==============================================================================

    private void Update() {
        CheckSpawnInput();
    }


    //==============================================================================
    // private
    //==============================================================================

    private void CheckSpawnInput(){
        if (XCI.GetButtonDown(XboxButton.A, XboxController.First) && m_PlayerInPlay[0] == false) {

        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Second ) && m_PlayerInPlay[1] == false){

        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Third) && m_PlayerInPlay[2] == false){

        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Fourth) && m_PlayerInPlay[3] == false){

        }
    }

    //==============================================================================
    // private
    //==============================================================================

    private void InstantiatePlayer() {

    }

    //==============================================================================
    private void SpawnPlayer() {

    }
}
