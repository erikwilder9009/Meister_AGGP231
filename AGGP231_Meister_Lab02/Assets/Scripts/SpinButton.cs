using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SpinButton : MonoBehaviour
{
    public GameObject Obj;
    bool pressed;
    bool up;

    void Start()
    {
        up = false;
        pressed = false;
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
    }
    private void Update()
    {
        if(pressed)
        {
            Obj.transform.Rotate(0, 75 * Time.deltaTime, 0);



            if (Obj.transform.localPosition.y <= .3)
            {
                up = true;
            }
            if (Obj.transform.localPosition.y >= .7)
            {
                up = false;
            }


            if (up)
            {
                Obj.transform.Translate(new Vector3(0, .33f * Time.deltaTime, 0));
            }
            if (!up)
            {
                Obj.transform.Translate(new Vector3(0, -.33f * Time.deltaTime, 0));
            }
        }
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if(pressed == false)
        {
            pressed = true;
            return;
        }


        if (pressed == true)
        {
            pressed = false;
            return;
        }
    }
}
