using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(2 * Time.deltaTime, 0, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Fire")
        {
            Destroy(gameObject);
            Debug.Log("burn");
        }
    }
}
