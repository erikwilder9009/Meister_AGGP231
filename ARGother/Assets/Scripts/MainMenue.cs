using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    public GameObject MMenue;
    public GameObject OMenue;

    AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Quit();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Options()
    {
        MMenue.SetActive(false);
        OMenue.SetActive(true);
    }
    public void Main()
    {
        MMenue.SetActive(true);
        OMenue.SetActive(false);
    }
    public void audioToggle()
    {
        if(AS.isPlaying)
        {
            AS.Stop();
        }
        if(!AS.isPlaying)
        {
            AS.Play();
        }
    }
}
