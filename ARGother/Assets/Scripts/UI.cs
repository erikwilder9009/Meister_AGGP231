using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update

    public static UI instance;
    public Sprite[] healthSprites;

    public GameObject pauseMenue;

    public Image healthVial;

    public Text value;

    public int money;
    public int health;


    void Start()
    {
        instance = this;
        money = 0;
        health = 6;

    }

    // Update is called once per frame
    void Update()
    {
        if (health > 6) { health = 6; }

        value.text = money.ToString();

        healthVial.sprite = healthSprites[health];


        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            pauseMenue.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void mainMenue()
    {
        SceneManager.LoadScene("Main Menue");
    }
    public void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
