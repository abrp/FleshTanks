﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private Projectile m_Projectile;

    [SerializeField]
    private Transform m_MuzzlePoint;

    [SerializeField]
    ParticleSystem m_MuzzleFlash;

    public void Shoot(float force) {
        Projectile p = Instantiate(m_Projectile, m_MuzzlePoint.position, Quaternion.identity);
        ParticleSystem muzzle = Instantiate(m_MuzzleFlash, m_MuzzlePoint.position, Quaternion.identity);
        Destroy(muzzle, 0.1f);
        p.GetComponent<Rigidbody>().AddForce(m_MuzzlePoint.forward * force);


    }
}
