using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public bool gameOver;
    public GameObject[] buttons;

    private EventSystem es;

    void Start()
    {
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            Resume();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            if (!AnyButtonSelected())
                es.SetSelectedGameObject(buttons[0]);
    }

    bool AnyButtonSelected()
    {
        for (int b = 0; b < buttons.Length; b++)
        {
            if (EventSystem.current.currentSelectedGameObject == buttons[b])
                return true;
        }
        return false;
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButtonSound()
    {
        //AudioManager.instance.Play("MenuButton");
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        //GameManager.Instance.m_CanvasManagerController.gameObject.SetActive(true);
        //GameManager.Instance.m_CanvasManagerController.m_textToDisplayAnim.SetActive(false);
        GameController.Instance.m_GamePaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
