using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Controls
    {
        [HideInInspector] public bool m_Jumping;

    }
    public Controls m_PlayerControls;

    public enum state { ALIVE, DEATH }

    public state m_CurrentState;
    [Range(0.0f, 20.0f)] public float m_JumpingForce;
    public float m_MovementSpeed;
    public float m_MaxVelocity;
    public float m_Gravity;

    public Collider2D m_TopCollider;
    public Collider2D m_BottomCollider;

    public Vector2 m_GravityDirection = Vector2.down;
    public Vector2 m_OriginalPosition;

    Rigidbody2D rb2d;
    bool m_Jumping;
    bool l_DiedOnce;

    void Start()
    {
        l_DiedOnce = false;
        m_Jumping = false;
        m_CurrentState = state.ALIVE;
        m_PlayerControls = new Controls();
        rb2d = GetComponent<Rigidbody2D>();
        m_OriginalPosition = this.gameObject.transform.position;
    }

    void Update()
    {
        CaptureControls();
        Act();
        Moving();
    }

    private void FixedUpdate()
    {
    }

    void Act()
    {
        switch (m_CurrentState)
        {
            case state.ALIVE:
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

        if (!m_Jumping && m_PlayerControls.m_Jumping)
        {
            m_Jumping = true;
            rb2d.AddForce(-m_GravityDirection * m_JumpingForce, ForceMode2D.Impulse);
        }

        rb2d.AddForce(m_GravityDirection * m_Gravity, ForceMode2D.Force);
        rb2d.velocity = new Vector2(Mathf.Clamp(m_MovementSpeed, 0f, m_MaxVelocity), rb2d.velocity.y);

    }

    void Jumping()
    {

    }

    void Death()
    {
        if (l_DiedOnce) return;
        Debug.Log("DEAD");
        rb2d.velocity = Vector2.zero;

        l_DiedOnce = true;
    }

    void CaptureControls()
    {
        m_PlayerControls.m_Jumping = Input.GetButtonDown("Jump");
    }

    public void SetOnGround(bool l_enable)
    {
        m_Jumping = l_enable;
    }

    public void ResetGame()
    {
        this.gameObject.transform.position = m_OriginalPosition;

        rb2d.velocity = Vector2.zero;

        m_CurrentState = state.ALIVE;

        l_DiedOnce = false;
    }
}
