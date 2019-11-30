using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Controls
    {
        public bool m_Jumping;

    }
    public Controls m_PlayerControls;

    public enum state { NONE, JUMPING, DEATH }

    public state m_CurrentState;
    [Range(0.0f, 20.0f)] public float m_JumpingForce;
    [Range(0.0f, 20.0f)] public float m_PlayerSpeed;
    public float m_Gravity;
    public Collider2D m_TopCollider;
    public Collider2D m_BottomCollider;
    public Vector2 m_GravityDirection = Vector2.down;
    float m_CurrentSpeed;
    public CameraController m_Camera;

    //CameraLight
    [HideInInspector] public Light cameraLight;


    Rigidbody2D m_Rb2d;
    bool m_Jumping;

    void Start()
    {
        m_Jumping = false;
        m_CurrentState = state.NONE;
        m_PlayerControls = new Controls();
        m_Rb2d = GetComponent<Rigidbody2D>();
        m_CurrentSpeed = m_PlayerSpeed;
        cameraLight = GetComponentInChildren<Light>();
        cameraLight.range = 0f;
    }

    void Update()
    {
        CaptureControls();
        Act();
        Moving();
    }

    private void FixedUpdate()
    {
        //m_Rb2d.MovePosition(m_Rb2d.position + Vector2.right * m_CurrentSpeed * Time.fixedDeltaTime);
        m_Rb2d.transform.Translate(Vector2.right * m_CurrentSpeed * Time.fixedDeltaTime);
        //m_Rb2d.AddForce(Vector2.right * 2 * m_CurrentSpeed, ForceMode2D.Force);
    }

    void Act()
    {
        switch (m_CurrentState)
        {
            case state.NONE:
                break;
            case state.JUMPING:
                Jumping();
                break;
            case state.DEATH:
                Death();
                break;
            default:
                //show errors
                break;
        }
    }

    void Moving()
    {
        if (m_CurrentState == state.DEATH)
            return;

        m_Rb2d.AddForce(m_GravityDirection * m_Gravity, ForceMode2D.Force);

        if (m_PlayerControls.m_Jumping)
            m_CurrentState = state.JUMPING;

    }

    void Jumping()
    {
        if (!m_Jumping)
            m_Rb2d.AddForce(-m_GravityDirection * m_JumpingForce, ForceMode2D.Impulse);

        m_CurrentState = state.NONE;
    }

    void Death()
    {

    }

    void CaptureControls()
    {
        m_PlayerControls.m_Jumping = Input.GetButtonDown("Jump");
    }

    public void SetOnGround(bool l_enable)
    {
        m_Jumping = l_enable;
    }
}
