using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnnouncement : MonoBehaviour {


    [SerializeField]
    private AudioClip m_Close;

    private AudioSource m_AudioSouce;

    private void Start() {
        m_AudioSouce = gameObject.AddComponent<AudioSource>();
    }

    public void Open() {
        CenterTextManager.instance.StartTextType("Gates are Open!", true);
    }

    public void Close() {
        m_AudioSouce.clip = m_Close;
        m_AudioSouce.Play();
        CenterTextManager.instance.StartTextType("Gates are Closing!",true);
    }


	
}
