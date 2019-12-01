using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionController : MonoBehaviour
{
    [SerializeField] private GameObject m_WhiteFadeOut;

    public void WhiteFadeOut()
    {
        m_WhiteFadeOut.GetComponent<Animator>().SetBool("WhiteFadeOut", true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Invoke("WhiteFadeOut", 1);
            GameController.Instance.m_EndGame = true;
        }
    }

    public void FadeToNextScene() // evento en la animación
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StopMusic()
    {
        AudioManager.instance.StopAllSounds();
    }
}
