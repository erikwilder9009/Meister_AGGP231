using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D RB;
    Vector2 movement;
    bool inBox;

    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Moved;


    // Start is called before the first frame update
    void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
        movement = new Vector2(5,0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetTouch(0).phase);

        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.name == "Ball" && inBox)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (RB.velocity.x < 0)
                {
                    RB.AddForce((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - mousePos2D) * 1000);
                }
                else
                {
                    RB.AddForce((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - mousePos2D) * 500);
                }
            }
        }


        //We check if we have more than one touch happening.
        //We also check if the first touches phase is Ended (that the finger was lifted)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase)
        {
            //We transform the touch position into word space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                if (touchedObject != null && touchedObject.transform.name == "Ball" && inBox)
                {
                    Debug.Log("Touched " + touchedObject.transform.name);
                    if (RB.velocity.x < 0)
                    {
                        RB.AddForce((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - touchPosWorld2D) * 1000);
                    }
                    else
                    {
                        RB.AddForce((new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - touchPosWorld2D) * 500);
                    }
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
