using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject Content;
    public Slider scaleSlider;
    public Slider rotationSlider;
    public Button playButton;
    public Button pauseButton; 
    public Button showButton;
    public Button hideButton;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
        scaleSlider.value = 1;
        rotationSlider.value = Content.transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        Content.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
        Content.transform.eulerAngles = new Vector3(Content.transform.eulerAngles.x, rotationSlider.value, Content.transform.eulerAngles.z);
    }

    public void Resume()
    {
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }
    public void Pause()
    {
        playButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
    public void Show()
    {
        hideButton.gameObject.SetActive(true);
        showButton.gameObject.SetActive(false);
    }
    public void Hide()
    {
        showButton.gameObject.SetActive(true);
        hideButton.gameObject.SetActive(false);
    }
}
