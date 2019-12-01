using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Controls
    {
        [HideInInspector] public bool m_Jumping;
        [HideInInspector] public bool m_ContinuousJumping;

    }
    public Controls m_PlayerControls;

    public enum state { ALIVE, DEATH }

    public state m_CurrentState;
    [Range(0.0f, 20.0f)] public float m_JumpingForce;

    public float m_MovementSpeed;
    public float m_MaxVelocity;
    public float m_Gravity;
    float m_CurrentGravity;
    public float m_MaxJumpDuration;

    public Collider2D m_TopCollider;
    public Collider2D m_BottomCollider;
    public CameraController m_Camera;

    //CameraLight
    [HideInInspector] public Light cameraLight;



    public Vector2 m_GravityDirection = Vector2.down;
    public Vector2 m_OriginalPosition;

    Rigidbody2D rb2d;
    bool m_Jumping;
    bool l_DiedOnce;
    bool m_JumpedOnce;
    float m_JumpTimer;

    void Start()
    {
        m_JumpTimer = 0f;
        m_JumpedOnce = false;
        l_DiedOnce = false;
        m_Jumping = false;
        m_CurrentState = state.ALIVE;
        m_PlayerControls = new Controls();
        cameraLight = GetComponentInChildren<Light>();
        cameraLight.range = 0f;
        rb2d = GetComponent<Rigidbody2D>();
        m_OriginalPosition = this.gameObject.transform.position;
        m_CurrentGravity = m_Gravity;
    }

    void Update()
    {
        if (GameController.Instance.m_gameStart && !GameController.Instance.m_GamePaused)
        {
            CaptureControls();
            Act();
            Moving();
            if (m_JumpTimer > 0f) m_JumpTimer -= Time.deltaTime;
        }
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

        if ((!m_Jumping && m_PlayerControls.m_Jumping) || (m_PlayerControls.m_ContinuousJumping && m_JumpedOnce))
        {
            if (m_JumpTimer < 0f)
            {
                m_JumpedOnce = false;
                m_JumpTimer = 0f;
            }
            else if (m_PlayerControls.m_Jumping)
            {
                rb2d.AddForce(-m_GravityDirection * m_JumpingForce, ForceMode2D.Impulse);
                m_JumpTimer = m_MaxJumpDuration;
                m_JumpedOnce = true;
                m_Jumping = true;
            }
            else if (m_PlayerControls.m_ContinuousJumping)
            {
                rb2d.AddForce(-m_GravityDirection * m_JumpingForce, ForceMode2D.Force);
            }
        }
        else
        {
            m_JumpedOnce = false;
            m_JumpTimer = 0f;
        }

        rb2d.AddForce(m_GravityDirection * m_CurrentGravity, ForceMode2D.Force);
        rb2d.velocity = new Vector2(Mathf.Clamp(m_MovementSpeed, 0f, m_MaxVelocity), rb2d.velocity.y);

    }

    void Death()
    {
        if (l_DiedOnce) return;
        Debug.Log("DEAD");
        rb2d.velocity = Vector2.zero;
        GameController.Instance.GameOver();
        l_DiedOnce = true;
    }

    void CaptureControls()
    {
        m_PlayerControls.m_Jumping = Input.GetButtonDown("Jump");
        m_PlayerControls.m_ContinuousJumping = Input.GetButton("Jump");
    }

    public void SetOnGround(bool l_enable)
    {
        m_Jumping = !l_enable;
    }

    public void ResetGame()
    {
        this.gameObject.transform.position = m_OriginalPosition;

        rb2d.velocity = Vector2.zero;

        rb2d.AddForce(Vector2.down * m_CurrentGravity, ForceMode2D.Impulse);

        m_CurrentState = state.ALIVE;

        m_JumpedOnce = false;
        m_Jumping = false;
        m_JumpTimer = 0f;
        l_DiedOnce = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Item")
        {
            Debug.Log("Item");
            ITakeItems l_Item = col.GetComponent<ITakeItems>();
            l_Item.TakeItem();
        }
    }
}
