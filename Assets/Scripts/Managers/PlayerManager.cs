﻿using System.Collections;
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
    private ParticleSystem m_SpawnParticleSystem;

    [SerializeField]
    private SpawnPoint[] m_SpawnPoints;

    [SerializeField]
    private float m_MoveSpeed = 2;

    [SerializeField]
    private float m_ShootSpeed = 5;

    [SerializeField]
    private float m_FireRate = 0.1f;

    private int m_CurrentSpawnIndex;

    [SerializeField]
    private GameObject[] m_PlayerUI;

    //==============================================================================
    // Delegates
    //==============================================================================

    public delegate void PlayerManagerEventHandler();
    public PlayerManagerEventHandler onInstantiatePlayerCallBack;

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
            Debug.Log("Spawn Player 1");
            m_PlayerInPlay[0] = true;
            m_PlayerUI[0].SetActive(true);
            InstantiatePlayer(XboxController.First);
        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Second ) && m_PlayerInPlay[1] == false){
            Debug.Log("Spawn Player 2");
            m_PlayerInPlay[1] = true;
            m_PlayerUI[1].SetActive(true);
            InstantiatePlayer(XboxController.Second);
        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Third) && m_PlayerInPlay[2] == false){
            Debug.Log("Spawn Player 3");
            m_PlayerInPlay[2] = true;
            m_PlayerUI[2].SetActive(true);
            InstantiatePlayer(XboxController.Third);
        }
        if (XCI.GetButtonDown(XboxButton.A, XboxController.Fourth) && m_PlayerInPlay[3] == false){
            Debug.Log("Spawn Player 4");
            m_PlayerInPlay[3] = true;
            m_PlayerUI[3].SetActive(true);
            InstantiatePlayer(XboxController.Fourth);
        }
    }

    //==============================================================================
    // private
    //==============================================================================

    private void InstantiatePlayer(XboxController controller) {
        Player p = Instantiate(m_PlayersPrefab, GetSpawnPosition(), Quaternion.identity);
        p.SetController(controller);
        p.SetShootSpeed(m_ShootSpeed);
        p.SetMoveSpeed(m_MoveSpeed);
        p.SetFireRate(m_FireRate);
        p.SetPlayerColor();

        // game state changed
        if (onInstantiatePlayerCallBack != null)
        {
            onInstantiatePlayerCallBack();
        }
    }

    public Vector3 GetSpawnPosition() {
        Vector3 position = m_SpawnPoints[m_CurrentSpawnIndex].transform.position;
        ParticleManager.instance.InstantiateParticleSystem(m_SpawnParticleSystem, position);
        IncrementSpawnIndex();
        return position;
    }

    //==============================================================================
    private void IncrementSpawnIndex() {
        m_CurrentSpawnIndex++;
        if (m_CurrentSpawnIndex > m_SpawnPoints.Length - 1) {
            m_CurrentSpawnIndex = 0;
        }
    }
}
