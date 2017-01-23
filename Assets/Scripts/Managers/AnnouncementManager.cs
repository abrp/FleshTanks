using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] m_AudioClips;           // General comments played when commentater have been silent for a while 

    [SerializeField]
    private float m_minDelayTime = 5.0f;
    [SerializeField]
    private float m_maxDelayTime = 10.0f;

    [SerializeField]
    private AudioClip[] m_AudioClipsLostArm;    // Played when player lost an arm piece
    [SerializeField]
    private AudioClip[] m_AudioClipsLostLeg;    // Played when player lost a leg piece 
    [SerializeField]
    private AudioClip[] m_AudioClipsLostTorsoe;     // ... and you get the drift
    [SerializeField]
    private AudioClip[] m_AudioClipsLostHead;
    [SerializeField]
    private AudioClip[] m_AudioClipsGateIsClosing;    // Played when gates are closing
    [SerializeField]
    private AudioClip[] m_AudioClipsGateIsOpening;    // ... and when they open

    private float m_timeToNextPlay;
    private AudioSource m_AudioSource;

    // Use this for initialization
    void Start () {

        m_AudioSource = GetComponent<AudioSource>();
        if(m_AudioSource == null)
            Debug.LogError("The [Announcement] field needs an Audio Source");

        if (m_AudioClips.Length > 0)
        {
            m_timeToNextPlay = Random.Range(m_minDelayTime, m_maxDelayTime);
            m_AudioSource.clip = m_AudioClips[0];
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_timeToNextPlay -= Time.deltaTime;
        if (m_timeToNextPlay < 0f)
        {
            playRandomClip(m_AudioClips);
            setDelayForNextGeneralPlay();
        }
    }

    //=============================
    // TODO Functions to be played at events
    //=============================

    void PlayLostArm()
    {
        playRandomClip(m_AudioClipsLostArm);
    }

    void PlayLostLeg()
    {
        playRandomClip(m_AudioClipsLostLeg);
    }

    void PlayLostTorsoe()
    {
        playRandomClip(m_AudioClipsLostTorsoe);
    }

    void PlayLostHead()
    {
        playRandomClip(m_AudioClipsLostHead);
    }

    void PlayGatesAreClosing()
    {
        playRandomClip(m_AudioClipsGateIsClosing);
    }

    //=============================
    // Privat
    //=============================

    private void playRandomClip(AudioClip[] ac)
    {
        if(ac != null && ac.Length > 0)
        {
            m_AudioSource.clip = ac[Random.Range(0, ac.Length - 1)];
            m_AudioSource.Play();
            setDelayForNextGeneralPlay();
        }
    }

    private void setDelayForNextGeneralPlay()
    {
        m_timeToNextPlay = Random.Range(m_minDelayTime, m_maxDelayTime);
    }
}
