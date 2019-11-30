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
    public GameObject m_PauseMenu;
    public GameObject m_GameOverMenu;
    [Range(0.1f, 2f)] public float m_VerticalOffsetTime;
    private CameraController m_Camera;
    [HideInInspector] public bool m_PlayerDied;
    [HideInInspector] public bool m_GamePaused;
    bool m_ChangeLight;
    KeyCode m_DebugLockKeyCode = KeyCode.O;

    void Awake()
    {
        m_GamePaused = false;
        m_PlayerGameObject = FindObjectOfType<PlayerController>().gameObject;
        m_PlayerComponents = new PlayerComponents();
        m_PlayerComponents.m_PlayerController = m_PlayerGameObject.GetComponent<PlayerController>();
        m_PlayerDied = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_Camera = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_PlayerComponents.m_PlayerController.m_GravityDirection *= -1;
            m_Camera.SetVerticalOffset(m_VerticalOffsetTime);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_ChangeLight = !m_ChangeLight;
            m_Camera.LightInterepolation(m_ChangeLight);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !m_GamePaused && !m_PlayerDied)
            Pause();

#if UNITY_EDITOR
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
#endif
    }

    void Pause()
    {
        m_PauseMenu.SetActive(true);
        m_GamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        //m_CanvasManagerController.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        //AudioManager.instance.StopAllSounds();
        m_GameOverMenu.SetActive(true);
        m_GamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}
