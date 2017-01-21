using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlesh : MonoBehaviour {

    //==============================================================================
    // Fields
    //==============================================================================

    private Collider m_Collder;
    private SkinnedMeshRenderer m_SkinnedMesh;
    private bool m_HasFlesh = true;

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    private void Start () {
        m_Collder = GetComponent<Collider>();
        m_SkinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    //==============================================================================
    // Public
    //==============================================================================

    public void RemoveFlesh () {
        m_Collder.enabled = false;
        m_SkinnedMesh.enabled = false;
        m_HasFlesh = false;
    }
}
