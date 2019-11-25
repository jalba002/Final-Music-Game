using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    private Collider2D m_ThisCollider;
    private PlayerController m_PlayerController;

    private void Start()
    {
        m_PlayerController = GetComponentInParent<PlayerController>();
        m_ThisCollider = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            m_PlayerController.SetOnGround(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            m_PlayerController.SetOnGround(true);
        }
    }
}
