using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public GameObject textbox;

    // Start is called before the first frame update
    void Start()
    {
        textbox.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 18)
        textbox.SetActive(true);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        textbox.SetActive(false);
    }
}
