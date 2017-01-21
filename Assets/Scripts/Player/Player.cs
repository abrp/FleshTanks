using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(PlayerController))]
public class Player : CustomMonobehavior {

    //==============================================================================
    // Fields
    //==============================================================================

    [SerializeField]
    private XboxController m_XboxController;

    private PlayerController m_PlayerController;

    [SerializeField]
    private int m_RespawnTime = 3;

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

    private bool m_IsAlive = true;

    private MeshRenderer[] meshRenderes;

    private bool m_IsRespawning = false;

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    private void Start() {
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStartPosition = transform.position;
    }

    //==============================================================================
    // private
    //==============================================================================

    private void HidePlayer() {
        Vector3 hidePosition = new Vector3(0, -5, 0);
        transform.position = hidePosition;
    }

    //==============================================================================
    public void SetController(XboxController controller) {
        m_XboxController = controller;
    }

    public void SetMoveSpeed(float f) {
        m_MoveSpeed = f;
    }

    public void SetShootSpeed(float f)
    {
        m_ShootSpeed = f;
    }

    public void SetFireRate(float f)
    {
        m_FireRate = f;
    }

    public void SetPlayerColor() {

    }

    //==============================================================================
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

    //==============================================================================
    private void HandleMovement() {
        float leftStickX = XCI.GetAxis(XboxAxis.LeftStickX, m_XboxController);
        float leftStickY = XCI.GetAxis(XboxAxis.LeftStickY, m_XboxController);

        Vector3 moveInput = new Vector3(leftStickX, 0, leftStickY);
        Vector3 moveVelocity = moveInput.normalized * m_MoveSpeed;

        m_PlayerController.Move(moveVelocity);
    }

    //==============================================================================
    private void HandleRespawn() {
        if (!m_IsRespawning) { 
            if (XCI.GetButtonDown(XboxButton.A, m_XboxController)) {
                StartCoroutine(StartRespawn());
            }
        }
    }

    //==============================================================================
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

    //==============================================================================
    private void Handle() {

    }

    //==============================================================================
    // Loops
    //==============================================================================

    protected override void PreLoop()
    {
        base.PreLoop();
    }

    protected override void GameLoop()
    {
        base.GameLoop();

        if (m_IsAlive)
        {
            HandleMovement();
            HandleRotation();
            HandleShooting();
        }
        else {
            HandleRespawn();
        }
    }

    //==============================================================================
    // public
    //==============================================================================

    public void Die() {
        HidePlayer();
        m_IsAlive = false;
    }

    //==============================================================================
    // IEnumerator
    //==============================================================================

    IEnumerator StartRespawn() {
        m_IsRespawning = true;
        yield return null;

        yield return new WaitForSeconds(m_RespawnTime);

        transform.position = PlayerManager.instance.GetSpawnPosition();

        // spawn some particles here

        m_IsRespawning = false;
        m_IsAlive = true;

    }
}
