using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class DrinkButton : MonoBehaviour
{
    public GameObject Obj;
    public GameObject fill;
    Renderer fillRender;
    bool pressed;

    int index;

    bool up;

    void Start()
    {
        index = 2;
        fillRender = fill.GetComponent<Renderer>();
        fillRender.material.SetColor("_Color", Color.red);
        up = false;
        pressed = false;
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
    }
    private void Update()
    {
        if (pressed)
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
        Debug.Log("Pressed");
        

        if (pressed == false)
        {
            pressed = true;

            if(index == 1)
            {
                fillRender.material.SetColor("_Color", Color.red);
                index++;

                return;
            }
            else if(index == 2)
            {
                fillRender.material.SetColor("_Color", Color.green);
                index++;

                return;
            }
            else if(index == 3)
            {
                fillRender.material.SetColor("_Color", Color.blue);
                index = 1;


                return;
            }
        }


        if (pressed == true)
        {
            pressed = false;
            return;
        }
    }
}
