﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlesh : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    private Collider m_Collder;
    private SkinnedMeshRenderer m_SkinnedMesh;
    private bool m_HasFlesh = true;

    [SerializeField]
    private ParticleSystem m_SmallFleshWound;
    [SerializeField]
    private ParticleSystem m_SmallBloodPool;

    private Player m_Player;

    public Player Owner
    {
        get { return m_Player; }
    }

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    private void Start () {
        m_Collder = GetComponent<Collider>();
        m_SkinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    public void SetPlayerReference(Player player) {
        m_Player = player;
    }

    //==============================================================================
    // Public
    //==============================================================================

    public void RemoveFlesh () {
        ParticleManager.instance.InstantiateParticleSystem(m_SmallFleshWound, this.transform.position);
        ParticleManager.instance.InstantiateParticleSystem(m_SmallFleshWound, new Vector3(this.transform.position.x, 0, this.transform.position.z));
        m_Collder.enabled = false;
        m_SkinnedMesh.enabled = false;
        m_HasFlesh = false;
        m_Player.RemovePiece();
    }

    public void ReFlesh() {
        m_Collder.enabled = true;
        m_SkinnedMesh.enabled = true;
        m_HasFlesh = true;
    }
}
