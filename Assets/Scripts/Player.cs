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
    private float moveSpeed = 2;

    [SerializeField]
    private Transform m_GunPoint;

    private Vector3 m_PlayerStartPosition;

    private bool m_isInPlay = true;

    private void Start() {
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStartPosition = transform.position;
    }

    private void HandleRotation() {

        float RightstickX = XCI.GetAxis(XboxAxis.RightStickX, m_XboxController);
        float RightstickY = XCI.GetAxis(XboxAxis.RightStickY, m_XboxController);

        Vector2 Rightstick = new Vector2(RightstickX, RightstickY);

        if (Rightstick.magnitude > 0.1)
        {
            float angle = Mathf.Atan2(RightstickY, RightstickX) * 57.297f;
            m_GunPoint.localEulerAngles = new Vector3(0, 90 - angle, 0);
        }
    }

    private void HandleMovement() {
        float LeftStickX = XCI.GetAxis(XboxAxis.LeftStickX, m_XboxController);
        float LeftStickY = XCI.GetAxis(XboxAxis.LeftStickY, m_XboxController);

        Vector3 moveInput = new Vector3(LeftStickX, 0, LeftStickY);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        m_PlayerController.Move(moveVelocity);
    }

    private void HandleShooting() {

        if (XCI.GetButtonDown(XboxButton.LeftBumper, m_XboxController) || XCI.GetButtonDown(XboxButton.RightBumper, m_XboxController))
        {
            m_Gun.Shoot(6000);
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
