﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    private Projectile m_Projectile;

    [SerializeField]
    private Transform m_GunPoint;

    [SerializeField]
    private Transform m_MuzzlePoint;

    [SerializeField]
    ParticleSystem m_MuzzleFlash;

    [SerializeField]
    AudioClip m_ShootSound;

    [SerializeField]
    

    public void Shoot(float force, Player player) {
        Projectile p = Instantiate(m_Projectile, m_MuzzlePoint.position, Quaternion.identity);
        //p.transform.rotation = m_GunPoint.rotation;
        p.SetProjectileRotation(m_MuzzlePoint.rotation);
        p.SetDirection(m_MuzzlePoint.forward);
        p.SetOwner(player);
        p.SetSpeed(force);
        ParticleManager.instance.InstantiateParticleSystem(m_MuzzleFlash, m_MuzzlePoint.position);
        SoundManager.instance.PlaySound(m_ShootSound);
        //p.GetComponent<Rigidbody>().AddForce(m_MuzzlePoint.forward * force);


    }
}
