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

    [SerializeField]
    private AudioClip m_NormalHitSound;
    [SerializeField]
    private AudioClip m_ExplosionHitSound;

    private Vector3 m_ForwardDirection;

    private float m_Speed = 1;

    float skin = 0.8f;

    public LayerMask collisionMask;

    public Rigidbody RigidBody {
        get { return m_Rigidbody; }
    }

	void Start () {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void CheckCollision()
    {

        RaycastHit hit;

        Debug.DrawRay(transform.position, m_ForwardDirection, Color.red);
        Debug.DrawRay(transform.position, -m_ForwardDirection, Color.red);
        Debug.DrawRay(transform.position, Vector3.left, Color.red);
        Debug.DrawRay(transform.position, Vector3.right, Color.red);
        Debug.DrawRay(transform.position, Vector3.up, Color.red);
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        bool hitsomething = false;

        if (Physics.Raycast(transform.position, m_ForwardDirection, out hit, skin, collisionMask))
        {
            hitsomething = true;
        }

        else if (Physics.Raycast(transform.position, -m_ForwardDirection, out hit, skin, collisionMask))
        {
            hitsomething = true;
        }
        else if (Physics.Raycast(transform.position, transform.up, out hit, skin, collisionMask))
        {
            hitsomething = true;
        }
        else if (Physics.Raycast(transform.position, transform.right, out hit, skin, collisionMask))
        {
            hitsomething = true;
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, skin, collisionMask))
        {
            hitsomething = true;
        }
        else if (Physics.Raycast(transform.position, -transform.forward, out hit, skin, collisionMask))
        {
            hitsomething = true;
        }

        if (hitsomething) {
            PlayExplosions();



            Destroy(gameObject);
        }

        /*RaycastHit2D[] hit = {
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
        }*/
    }

    public void SetDirection(Vector3 direction) {
        m_ForwardDirection = direction;
    }

    public void SetSpeed(float speed) {
        m_Speed = speed;
    }

    private void FixedUpdate() {
        float moveDistance = Time.deltaTime * m_Speed;
        //Vector3 moveDirection = transform.forward;
        transform.Translate(m_ForwardDirection * moveDistance);
        CheckCollision();
    }

    /*private void OnTriggerStay(Collider other) {
 
        if (other.GetComponent<Wall>()) {
            PlayExplosions();
            Destroy(gameObject);
        }

        if (other.GetComponent<Player>()){
           // other.GetComponent<Player>().Respawn();
            PlayExplosions();
            Destroy(gameObject);

        }
    }*/

    private void PlayExplosions() {
        int chanceForExplosion = Random.Range(0, 5);
        if (chanceForExplosion == 0)
        {
           
            ParticleManager.instance.InstantiateParticleSystem(explosion, transform.position);
            ParticleManager.instance.InstantiateParticleSystem(smoke, transform.position);
            SoundManager.instance.PlaySound(m_ExplosionHitSound);
            CameraAnimationShaker.instance.PlayLargeShake();
            //soundManager.PlayExplosionSoundSound();*/
        }
        else
        {
            ParticleManager.instance.InstantiateParticleSystem(normalHit, transform.position);
            ParticleManager.instance.InstantiateParticleSystem(lesserSmoke, transform.position);
            CameraAnimationShaker.instance.PlayGentleShake();
            SoundManager.instance.PlaySound(m_NormalHitSound);
            //soundManager.PlayNormalHitSoundSound();*/
        }
        
        
    }
}

