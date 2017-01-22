using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_partstart : MonoBehaviour {

	// Use this for initialization
	public ParticleSystem m_particleSystem;



	public void Stop(){
		m_particleSystem.Stop ();
	}

	public void Start(){
		m_particleSystem.Play ();
	}

	public void Play(){
		m_particleSystem.Play ();
	}


}
