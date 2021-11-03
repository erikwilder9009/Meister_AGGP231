using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEye : MonoBehaviour
{

    bool toTheLeft;

    int rotation = 110;
    public float pauseDelay;
    float pauseTime;
    bool paused;

    public GameObject actualEye;

    public int health;


    float hoverMove;
    bool up;

    // Start is called before the first frame update
    void Start()
    {
        toTheLeft = true;
        pauseTime = Time.time;
        paused = true;

        hoverMove = 0;
        up = true;
    }

    // Update is called once per frame
    void Update()
    {
        Hover();

        if (pauseDelay < Time.time - pauseTime)
        {
            paused = false;
        }


        if(transform.rotation.eulerAngles.z <= 1f && !paused && !toTheLeft)
        {
            gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
            toTheLeft = true;
            pauseTime = Time.time;
            paused = true;
        }

        if(transform.rotation.eulerAngles.z > 110 && !paused && toTheLeft)
        {
            toTheLeft = false;
            pauseTime = Time.time;
            paused = true;
        }


        if (toTheLeft && !paused)
        {
            transform.Rotate(new Vector3(0, 0, rotation / 2) * Time.deltaTime);
            actualEye.transform.Rotate(new Vector3(0, 0, -rotation / 2) * Time.deltaTime);
        }
        else if (!toTheLeft && !paused)
        {
            transform.Rotate(new Vector3(0, 0, -rotation / 2) * Time.deltaTime);
            actualEye.transform.Rotate(new Vector3(0, 0, rotation / 2) * Time.deltaTime);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    void Hover()
    {
        if (up)
        {
            actualEye.transform.Translate(new Vector2(0, .1f)) ;
            hoverMove += .1f;
        }
        else if (!up)
        {
            actualEye.transform.Translate(new Vector2(0, -.1f));
            hoverMove -= .1f;
        }


        if (hoverMove >= 5)
        {
            up = false;
        }
        if (hoverMove <= 0)
        {
            up = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            health -= 1;
        }
    }
}
