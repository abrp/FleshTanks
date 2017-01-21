using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationShaker : MonoBehaviour {

    public static CameraAnimationShaker instance;

    private Animator m_Animator;

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

    private void Start () {
        m_Animator = GetComponent<Animator>();
    }

    public void PlayGentleShake() {
        m_Animator.SetTrigger("gentleshake");
    }

    public void PlayLargeShake()
    {
        m_Animator.SetTrigger("largeshake");
    }
}
