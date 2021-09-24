using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GuyButton : MonoBehaviour
{
    Animator animate;
    public GameObject obj;

    void Start()
    {
        animate = obj.GetComponent<Animator>();
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        animate.Play("GuyFall");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        animate.Play("GuyNone");
    }
}
