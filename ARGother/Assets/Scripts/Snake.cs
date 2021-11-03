using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    Rigidbody2D rb;

    public float movement;
    float movementU;
    float facing = 8;
    LayerMask layermask;
    bool doing;
    bool dead;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        layermask = LayerMask.GetMask("Ground") + LayerMask.GetMask("Pushable");
        animator = GetComponent<Animator>();

        if (transform.rotation.eulerAngles.y == 180)
        {
            facing = -8;
            movementU = -movement;
        }
        else
        {
            facing = 8;
            movementU = movement;
        }

    }

    // Update is called once per frame
    void Update()
    {
        doing = false;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 3.5f), new Vector2(1, 0), facing, layermask);
        if (hit && (transform.rotation.eulerAngles.y == 180 || transform.rotation.eulerAngles.y == -180))
        {
            doing = true;
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 3.5f), new Vector2(facing, 0), Color.red);
            transform.eulerAngles = new Vector3 (0, 0, 0);
            facing = 8;
            movementU = movement;
        }
        if (hit && transform.rotation.eulerAngles.y == 0 && !doing)
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 3.5f), new Vector2(facing, 0), Color.red);
            transform.eulerAngles = new Vector3(0, 180, 0);
            facing = -8;
            movementU = -movement;
        }
        else
        {
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 3.5f), new Vector2(facing, 0), Color.green);
        }


        if(!dead)
        {
            rb.velocity = new Vector2(movementU, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-(movementU/2), rb.velocity.y);
        }


        animator.SetBool("Dead", dead);
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.collider, true);
        }
        if (collision.gameObject.layer == 15)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.collider, true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            dead = true;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(gameObject, .75f);
        }
    }
}
