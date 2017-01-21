using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    //==============================================================================
    // Fields
    //==============================================================================

    public static SoundManager instance = null;

    [SerializeField]
    private int m_NumberOfAudioSources = 15;

    private AudioSource[] m_AudioSources;

    private int m_AudioIndex = 0;

    //==============================================================================
    // MonoBehavours
    //==============================================================================

    //==============================================================================
    public void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    //==============================================================================
    private void Start()
    {
        SetupAudioSources();
    }

    //==============================================================================
    // Private
    //==============================================================================

    //==============================================================================
    private void SetupAudioSources()
    {

        m_AudioSources = new AudioSource[m_NumberOfAudioSources];

        for (int i = 0; i < m_NumberOfAudioSources; i++)
        {
            GameObject go = new GameObject();
            m_AudioSources[i] = go.AddComponent<AudioSource>();
            go.transform.SetParent(this.transform);
            go.name = "AudioSource: " + i;
        }
    }

    //==============================================================================
    // Sound Methods
    //==============================================================================

    //==============================================================================
    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            m_AudioSources[m_AudioIndex].clip = audioClip;
            m_AudioSources[m_AudioIndex].Play();

            m_AudioIndex++;

            if (m_AudioIndex >= m_NumberOfAudioSources)
            {
                m_AudioIndex = 0;
            }
        }
    }
}
