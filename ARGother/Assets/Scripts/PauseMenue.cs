using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenue : MonoBehaviour
{
    public AudioSource AS;

    public GameObject overlay;


    public void mainMenue()
    {
        Destroy(Movement.instance.gameObject);
        SceneManager.LoadScene("Main Menue");
        Time.timeScale = 1;
    }
    public void resume()
    {
        gameObject.SetActive(false);
        overlay.SetActive(true);
        Time.timeScale = 1;
    }

    public void audioToggle()
    {
        if (AS.isPlaying == false)
        {
            AS.Play();
            return;
        }
        if (AS.isPlaying)
        {
            AS.Stop();
            return;
        }
    }
}
