using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    GameObject m_PlayerGameObject;

    [System.Serializable]
    public class PlayerComponents
    {
        public PlayerController m_PlayerController;
    }

    public PlayerComponents m_PlayerComponents;

    void Start()
    {
        m_PlayerGameObject = FindObjectOfType<PlayerController>().gameObject;
        m_PlayerComponents = new PlayerComponents();
        m_PlayerComponents.m_PlayerController = m_PlayerGameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_PlayerComponents.m_PlayerController.m_GravityDirection *= -1;
        }
    }
}
