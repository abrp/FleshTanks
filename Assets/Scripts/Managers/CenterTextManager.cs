using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenterTextManager : MonoBehaviour {

    public static CenterTextManager instance = null;

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

    public float letterPause = 0.2f;
    //public AudioClip typeSound1;
    //public AudioClip typeSound2;

    private Text m_Text;

    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    public void StartTextType(string message, bool autoremove) {
        m_Text.text = "";
        StopAllCoroutines();
        StartCoroutine(TypeText(message, autoremove));
    }

    IEnumerator TypeText(string message, bool autoremove)
    {
        foreach (char letter in message.ToCharArray())
        {
            m_Text.text += letter;
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }

        if (autoremove) {
            yield return new WaitForSeconds(3);
            m_Text.text = "";
        }
    }
}
