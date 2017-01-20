using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private Projectile m_Projectile;

    [SerializeField]
    private Transform m_MuzzlePoint;

    public void Shoot(float force) {
        Projectile p = Instantiate(m_Projectile, m_MuzzlePoint.position, Quaternion.identity);
        p.GetComponent<Rigidbody>().AddForce(m_MuzzlePoint.forward * force);
    }
}
