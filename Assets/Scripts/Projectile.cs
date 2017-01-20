using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class Projectile : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    private ParticleSystem m_HitParticles;

    public Rigidbody RigidBody {
        get { return m_Rigidbody; }
    }

	void Start () {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
 
        if (other.GetComponent<Wall>()) {
            Destroy(gameObject);
        }

        if (other.GetComponent<Player>()){
            other.GetComponent<Player>().Respawn();
        }
    }
}
