using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    private void Awake()
    {
        if(instance != this)
        {
            Destroy(instance);
        }
        instance = this;
        
        //if(instance == this)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}
    }

    public void LoadBall()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMonkey()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
