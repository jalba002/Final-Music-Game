using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameController.Instance.m_PlayerComponents.m_PlayerController.gameObject)
        {
            GameController.Instance.m_PlayerComponents.m_PlayerController.m_CurrentState = PlayerController.state.DEATH;
        }
    }
}

