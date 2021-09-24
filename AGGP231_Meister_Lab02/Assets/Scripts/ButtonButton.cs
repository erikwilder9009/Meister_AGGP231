using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonButton : MonoBehaviour
{
    public GameObject Obj;
    bool pressed;
    Vector3 startLoc;

    void Start()
    {
        startLoc = Obj.transform.localPosition;
        pressed = false;
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }
    private void Update()
    {
        if (pressed)
        {
            if (Obj.transform.localPosition.y > 0)
            {
                Debug.Log(Obj.transform.localPosition);
                Obj.transform.Translate(new Vector3(0, -.1f * Time.deltaTime, 0));
            }
            if (Obj.transform.localPosition.y <= 0)
            {
                pressed = false;
            }
        }
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        pressed = true;
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        pressed = false;
        Obj.transform.localPosition = startLoc;
    }
}
