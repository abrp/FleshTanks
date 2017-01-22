using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] m_AudioClips;

    [SerializeField]
    private AudioClip[] m_AudioClipsGates;

    private AudioSource m_AudioSource;

    private float minDelayTime = 3.0f;
    private float maxDelayTime = 6.0f;
    private float timeToNextPlay;

    // Use this for initialization
    void Start () {

        m_AudioSource = GetComponent<AudioSource>();

        if (m_AudioClips.Length > 0)
        {
            timeToNextPlay = Random.Range(minDelayTime, maxDelayTime);
            m_AudioSource.clip = m_AudioClips[0];
        }
    }
	
	// Update is called once per frame
	void Update () {
        timeToNextPlay -= Time.deltaTime;
        if (timeToNextPlay < 0f)
        {
            m_AudioSource.clip = m_AudioClips[Random.Range(0, m_AudioClips.Length - 1)];
            m_AudioSource.Play();
            timeToNextPlay = Random.Range(minDelayTime, maxDelayTime);
            
        }
    }

    void PlayGatesAreClosing()
    {
        m_AudioSource.clip = m_AudioClipsGates[Random.Range(0, m_AudioClipsGates.Length - 1)];
        m_AudioSource.Play();
    }
}
