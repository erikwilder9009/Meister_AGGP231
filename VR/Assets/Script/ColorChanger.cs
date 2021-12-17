using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    MeshRenderer MR;
    TrailRenderer TR;
    float timer;
    public float changeDelay;

    // Start is called before the first frame update
    void Start()
    {
        MR = gameObject.GetComponent<MeshRenderer>();
        TR = gameObject.GetComponent<TrailRenderer>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    { 
        if(timer > changeDelay)
        {
            MR.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            TR.startColor = MR.material.color;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
