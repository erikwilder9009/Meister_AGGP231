using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    Rigidbody2D RB;
    public float moveSpeed;
    public float jumpHeight;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        grounded = true;
    }


    public void Left()
    {
        Debug.Log("Left");
        RB.velocity = new Vector2(-moveSpeed, RB.velocity.y);
    }
    public void Right()
    {
        Debug.Log("Right");
        RB.velocity = new Vector2(moveSpeed, RB.velocity.y);
    }
    public void ButtonUp()
    {
        RB.velocity = new Vector2(0, RB.velocity.y);
    }
    public void Jump()
    {
        if(grounded)
        {
            RB.AddForce(new Vector2(RB.velocity.x, jumpHeight));
        }
        grounded = false;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Bricks")
        {
            grounded = true;
        }
    }
}
