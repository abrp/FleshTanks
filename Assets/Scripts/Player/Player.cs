using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XboxCtrlrInput;		// Be sure to include this if you want an object to have Xbox input

[RequireComponent (typeof(PlayerController))]
public class Player : MonoBehaviour {

    [SerializeField]
    private XboxController m_XboxController;

    private PlayerController m_PlayerController;

    [SerializeField]
    private Gun m_Gun;

    [SerializeField]
    private float m_MoveSpeed = 2;

    [SerializeField]
    private float m_ShootSpeed = 5;

    [SerializeField]
    private float m_FireRate = 0.1f;

    private float m_NextFire = 0;

    [SerializeField]
    private Transform m_GunPoint;

    private Vector3 m_PlayerStartPosition;

    private bool m_isInPlay = true;

    private void Start() {
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStartPosition = transform.position;
    }

    private void HandleRotation() {

        float rightstickX = XCI.GetAxis(XboxAxis.RightStickX, m_XboxController);
        float rightstickY = XCI.GetAxis(XboxAxis.RightStickY, m_XboxController);

        Vector2 rightstick = new Vector2(rightstickX, rightstickY);

        if (rightstick.magnitude > 0.1)
        {
            float angle = Mathf.Atan2(rightstickY, rightstickX) * 57.297f;
            m_GunPoint.localEulerAngles = new Vector3(0, 90 - angle, 0);
        }
    }

    private void HandleMovement() {
        float leftStickX = XCI.GetAxis(XboxAxis.LeftStickX, m_XboxController);
        float leftStickY = XCI.GetAxis(XboxAxis.LeftStickY, m_XboxController);

        Vector3 moveInput = new Vector3(leftStickX, 0, leftStickY);
        Vector3 moveVelocity = moveInput.normalized * m_MoveSpeed;

        m_PlayerController.Move(moveVelocity);
    }

    private void HandleShooting() {

        if (m_NextFire < Time.time) {

            float leftTrigger = XCI.GetAxis(XboxAxis.LeftTrigger, m_XboxController);
            float rightTrigger = XCI.GetAxis(XboxAxis.RightTrigger, m_XboxController);

            if ((leftTrigger > 0.1f) || (rightTrigger > 0.1f)) {
                m_Gun.Shoot(m_ShootSpeed);
                m_NextFire = Time.time + m_FireRate;
            }
        }
    }

	private void Update () {
        if (m_isInPlay) {
            HandleMovement();
            HandleRotation();
            HandleShooting();
        }
    }

    public void Respawn() {

        transform.position = m_PlayerStartPosition;
    }

    IEnumerator StartRespawn() {
        yield return null;
    }
}
