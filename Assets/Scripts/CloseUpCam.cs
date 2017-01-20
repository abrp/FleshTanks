using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpCam : MonoBehaviour {

    [SerializeField]
    private Transform m_CameraPosition;

	// Use this for initialization
	void Start () {
        transform.SetParent(m_CameraPosition);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
