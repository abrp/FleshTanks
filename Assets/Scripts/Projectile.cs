using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class Projectile : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    public ParticleSystem explosion;
    public ParticleSystem normalHit;
    public ParticleSystem smoke;
    public ParticleSystem lesserSmoke;

    float speed = 550;
    float spread = 1;
    float skin = .4f;
    public LayerMask collisionMask;

    public Rigidbody RigidBody {
        get { return m_Rigidbody; }
    }

	void Start () {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void CheckCollision()
    {
        RaycastHit2D[] hit = {
            Physics2D.Raycast (transform.localPosition, Vector2.left, skin, collisionMask),
            Physics2D.Raycast (transform.localPosition, Vector2.down, skin, collisionMask),
            Physics2D.Raycast (transform.localPosition, Vector2.left, skin, collisionMask),
            Physics2D.Raycast (transform.localPosition, Vector2.right, skin, collisionMask),
        };

        Debug.DrawRay(transform.localPosition, Vector2.up * skin, Color.red);
        Debug.DrawRay(transform.localPosition, Vector2.down * skin, Color.red);
        Debug.DrawRay(transform.localPosition, Vector2.left * skin, Color.red);
        Debug.DrawRay(transform.localPosition, Vector2.right * skin, Color.red);

        for (int i = 0; i < hit.Length; i++)
        {
            
            if (hit[i].collider != null)
            {
                PlayExplosions();

            }
        }
    }

    private void Update() {
        //CheckCollision();
    }

    private void OnTriggerStay(Collider other) {
 
        if (other.GetComponent<Wall>()) {
            PlayExplosions();
            Destroy(gameObject);
        }

        if (other.GetComponent<Player>()){
           // other.GetComponent<Player>().Respawn();
            PlayExplosions();
            Destroy(gameObject);

        }
    }

    private void PlayExplosions() {
        int chanceForExplosion = Random.Range(0, 5);
        if (chanceForExplosion == 0)
        {
           
            ParticleManager.instance.InstantiateParticleSystem(explosion, transform.position);
            ParticleManager.instance.InstantiateParticleSystem(smoke, transform.position);
            //cameraAnimation.PlayLargeShake();
            //soundManager.PlayExplosionSoundSound();*/
        }
        else
        {
            ParticleManager.instance.InstantiateParticleSystem(normalHit, transform.position);
            ParticleManager.instance.InstantiateParticleSystem(lesserSmoke, transform.position);
            //soundManager.PlayNormalHitSoundSound();*/
        }
        
        
    }
}

