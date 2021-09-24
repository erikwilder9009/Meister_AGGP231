using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CapnButton : MonoBehaviour
{
    public GameObject bulletspawn;
    public GameObject bullet;

    void Start()
    {
        this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        GameObject obj = Instantiate(bullet, bulletspawn.transform.position, bulletspawn.transform.rotation);
        Destroy(obj, 2);
    }
}