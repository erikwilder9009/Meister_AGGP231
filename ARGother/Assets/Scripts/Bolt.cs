using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{


    public Sprite[] SpriAnimes = new Sprite[] { };
    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    public float velocity;
    float velocityU;




    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(Animate), .25f, .25f);

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y == 180)
        {
            velocityU = -velocity;
        }
        else
        {
            velocityU = velocity;
        }

        rb.velocity = new Vector2(-velocityU, 0);
    }

    void Animate()
    {
        int index = Random.Range(0, SpriAnimes.Length);
        spriteRenderer.sprite = SpriAnimes[index];
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9 || other.gameObject.layer == 14 || other.gameObject.layer == 15)
        {
            Destroy(gameObject);
        }
    }
}

