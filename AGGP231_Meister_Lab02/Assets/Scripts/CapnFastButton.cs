using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CapnFastButton : MonoBehaviour
{
    public GameObject bulletspawn;
    public GameObject bullet;
    bool firing;
    public float FireDelay;
    float firedelay;

    void Start()
    {
        firedelay = 0;
        firing = false;
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }

    private void FixedUpdate()
    {
        firedelay += 1 * Time.deltaTime;
        if(firing && firedelay > FireDelay)
        {
            GameObject obj = Instantiate(bullet, bulletspawn.transform.position, bulletspawn.transform.rotation);
            Destroy(obj, 2);
            firedelay = 0;
        }
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        firing = true;
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        firing = false;
    }
}
