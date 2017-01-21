using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private Vector3 m_velocity;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        m_velocity = velocity;
    }

    private void FixedUpdate()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_velocity * Time.fixedDeltaTime);
    }

}
