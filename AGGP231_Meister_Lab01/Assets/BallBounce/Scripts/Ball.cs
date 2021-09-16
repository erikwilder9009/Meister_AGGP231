using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D RB;
    Vector2 movement;
    bool inBox;
    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        movement = new Vector2(5,0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(RB.velocity);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.name == "Ball" && inBox)
            {
                Debug.Log(hit.collider.gameObject.name);
                if(RB.velocity.x < 0)
                {
                    RB.AddForce((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - mousePos2D) * 1000);
                }
                else
                {
                    RB.AddForce((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - mousePos2D) * 500);
                }
            }
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "FallArea")
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "HitArea")
        {
            inBox = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "HitArea")
        {
            inBox = false;
        }
    }

    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.name == "ball")
        {
            Debug.Log("Clicked on ball");
        }
    }
}
