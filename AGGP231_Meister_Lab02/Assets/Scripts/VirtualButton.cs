using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButton : MonoBehaviour
{
    public GameObject Obj;
    bool pressed;

    void Start()
    {
        pressed = false;
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }
    private void Update()
    {
        if(pressed)
        {
            Obj.transform.Rotate(0, 5, 0);
        }
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        pressed = true;
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        pressed = false;
    }
}
