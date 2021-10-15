using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    Rigidbody RB;
    public float moveSpeed;
    public float jumpHeight;
    bool grounded;
    float moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = 0;
        RB = gameObject.GetComponent<Rigidbody>();
        grounded = true;
    }

    private void Update()
    {
        RB.velocity = new Vector3(-moveSpeed * moving, RB.velocity.y, 0);
    }


    public void Left()
    {
        moving = -1;
    }
    public void Right()
    {
        moving = 1;
    }
    public void ButtonUp()
    {
        moving = 0;
        RB.velocity = new Vector3(0, RB.velocity.y,0);
    }
    public void Jump()
    {
        if(grounded)
        {
            RB.AddForce(new Vector3(RB.velocity.x, jumpHeight,0));
        }
        grounded = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bricks")
        {
            grounded = true;
        }
        if (collision.gameObject.name == "Fire")
        {
            Destroy(gameObject);
        }
    }


}
