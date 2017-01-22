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

    [SerializeField]
    private ParticleSystem m_PlayerToken;

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
    private int m_Lives = 3;

    [SerializeField]
    private float m_FireRate = 0.1f;

    private float m_NextFire = 0;

    [SerializeField]
    private float m_Ammo = 50;

    [SerializeField]
    private int m_Score = 0;

    [SerializeField]
    private Transform m_GunPoint;

    [SerializeField]
    private Transform m_Skeleton;

    [SerializeField]
    private Animator m_HarpoonAnimator;

    [SerializeField]
    private Animator m_FleshTankAnimator;

    [SerializeField]
    private bool m_IsDonePlaying = false;

    private Vector3 m_PlayerStartPosition;

    private bool m_IsAlive = true;

    private PlayerFlesh[] m_PlayerFlesh;

    private int m_PiecesOfFlesh;

    private bool m_CanRespawn = false;

    private PlayerUI m_PlayerUI;

    private int m_PlayerNumber;

    private Rigidbody m_RigidBody;

    public bool IsAlive {
        get { return m_IsAlive; }
    }

    public bool IsDonePlaying {
        get { return m_IsDonePlaying; }
    }

    public int PlayerNumber {
        get { return m_PlayerNumber; }
    }

    //==============================================================================
    // MonoBehaviour
    //==============================================================================

    private void Start() {
        m_PlayerController = GetComponent<PlayerController>();
        m_PlayerStartPosition = transform.position;
        m_PlayerFlesh = GetComponentsInChildren<PlayerFlesh>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_PiecesOfFlesh = m_PlayerFlesh.Length;

        for (int i = 0; i < m_PlayerFlesh.Length; i++)
        {
            m_PlayerFlesh[i].SetPlayerReference(this);
        }
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

    public void AddScore(int point) {
        m_Score += point;
        m_PlayerUI.UpdateScore(m_Score);
    }

    public void SetPlayerUI(PlayerUI playerUI)
    {
        m_PlayerUI = playerUI;
    }

    public void SetPlayerNumber(int number) {
        m_PlayerNumber = number;
    }

    public void SetMoveSpeed(float f) {
        m_MoveSpeed = f;
    }

    public void SetShootSpeed(float f)
    {
        m_ShootSpeed = f;
    }

    public void SetColor(Color color)
    {
        m_PlayerToken.startColor = color;
    }

    public void SetFireRate(float f)
    {
        m_FireRate = f;
    }

    public void RemovePiece() {
        m_PiecesOfFlesh--;
        if (m_PiecesOfFlesh == 0) {
            Die();
        }
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
        Vector3 moveVelocity = moveInput * m_MoveSpeed;

        Vector2 leftStick = new Vector2(leftStickX, leftStickY);

        if (leftStick.magnitude > 0.1)
        {
            float angle = Mathf.Atan2(leftStickX, leftStickY) * 57.297f;
            m_Skeleton.localEulerAngles = new Vector3(0, angle, 0);
            m_FleshTankAnimator.SetFloat("backForward", leftStick.magnitude);
        }
        else {
            m_FleshTankAnimator.SetFloat("backForward", 0);
        }

        m_PlayerController.Move(moveVelocity);
    }

    //==============================================================================
    private void HandleRespawn() {
        if (XCI.GetButtonUp(XboxButton.A, m_XboxController)) {
            Respawn();
        }
    }

    //==============================================================================
    private void HandleShooting() {

        if (m_NextFire < Time.time) {

            float leftTrigger = XCI.GetAxis(XboxAxis.LeftTrigger, m_XboxController);
            float rightTrigger = XCI.GetAxis(XboxAxis.RightTrigger, m_XboxController);

            if ((leftTrigger > 0.1f) || (rightTrigger > 0.1f)) {
                if (m_Ammo > 0) {
                    if (m_HarpoonAnimator != null) {
                        m_HarpoonAnimator.SetTrigger("shoot");
                    }

                    m_Gun.Shoot(m_ShootSpeed,this);
                    //m_Ammo--;
                    m_NextFire = Time.time + m_FireRate;
                }
            }
        }
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

        else  {
            HandleRespawn();
        }
    }

    protected override void EndLoop()
    {
        m_PlayerController.Move(Vector3.zero);
    }

    //==============================================================================
    // public
    //==============================================================================

    public void Die() {
        HidePlayer();
        m_Lives--;
        m_PlayerUI.UpdateLives(m_Lives);
        m_IsAlive = false;

        CenterTextManager.instance.StartTextType("Player " + m_PlayerNumber + " died", true);

        m_RigidBody.useGravity = false;
        m_RigidBody.velocity = Vector3.zero;

        if (m_Lives == 0) {
            m_IsDonePlaying = true;
            CenterTextManager.instance.StartTextType("Player " + m_PlayerNumber + " is DONE!!", true);
        }

        GameManager.instance.CheckIfGameWon();
    }

    private void Respawn()
    {
        if (m_Lives > 0)
        {
            transform.position = PlayerManager.instance.GetSpawnPosition();
            
            m_PiecesOfFlesh = m_PlayerFlesh.Length;

            for (int i = 0; i < m_PlayerFlesh.Length; i++)
            {
                m_PlayerFlesh[i].ReFlesh();
            }

            m_RigidBody.useGravity = true;

            CenterTextManager.instance.StartTextType("Player " + m_PlayerNumber + " respawned", true);
            m_IsAlive = true;
        }
    }
}
