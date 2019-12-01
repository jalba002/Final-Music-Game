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
    [HideInInspector] public CameraController m_Camera;
    [HideInInspector] public bool m_PlayerDied;
    [HideInInspector] public bool m_GamePaused;
    [HideInInspector] public bool m_ChangeLight;
    [HideInInspector] public bool m_HasDLight;
    [HideInInspector] public bool m_HasGravity;
    KeyCode m_DebugLockKeyCode = KeyCode.O;
    int m_CDTimer;
    public CanvasManager m_CanvasManagerController;
    [HideInInspector] public bool m_gameStart;
    [HideInInspector] public bool m_allowRestart;
    public Animator BlackFadeOut;


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
        m_CDTimer = 3;
        StartCoroutine(CountDownToStart());
        BlackFadeOut.Play("BlackFadeOut");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_PlayerComponents.m_PlayerController.m_GravityDirection *= -1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_ChangeLight = !m_ChangeLight;
            m_Camera.LightInterepolation(m_ChangeLight);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !m_GamePaused && !m_PlayerDied && m_allowRestart)
            Pause();

        if (Input.GetKeyDown(KeyCode.R))
            Restart();

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
        AudioManager.instance.Pause("FullSong");
    }

    public void Restart()
    {
        if (m_HasGravity)
        {
            m_PlayerComponents.m_PlayerController.m_GravityDirection = Vector2.down;
            m_HasGravity = false;
        }

        m_gameStart = m_allowRestart = false;
        m_PlayerComponents.m_PlayerController.ResetGame();
        m_CDTimer = 3;
        StartCoroutine(CountDownToStart());
        Cursor.visible = false;
        m_CanvasManagerController.m_GameScore = 0.0f;
        m_CanvasManagerController.CanvasScore();
        BlackFadeOut.Play("BlackFadeOut");
        AudioManager.instance.StopAllSounds();
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

    IEnumerator CountDownToStart()
    {
        while (m_CDTimer > 0)
        {
            if (m_CDTimer == 2)
                if (m_HasDLight)
                {
                    m_ChangeLight = false;
                    m_Camera.LightInterepolation(m_ChangeLight);
                    m_HasDLight = false;
                }

            m_CanvasManagerController.m_CoutDownTimerText.gameObject.SetActive(true);
            m_CanvasManagerController.m_CoutDownTimerText.text = m_CDTimer.ToString();
            yield return new WaitForSeconds(1f);
            m_CDTimer--;
        }

        AudioManager.instance.Play("FullSong");
        m_CanvasManagerController.m_CoutDownTimerText.text = "GO!";
        m_gameStart = true;
        yield return new WaitForSeconds(1f); // un segundo después de empezar el evento te permite iniciarlo de nuevo
        m_CanvasManagerController.m_CoutDownTimerText.gameObject.SetActive(false);
        AllowRestartGame();
    }

    void AllowRestartGame()
    {
        m_allowRestart = true;
    }
}
