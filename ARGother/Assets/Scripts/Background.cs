using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background instance;

    public Vector2 movementMulti;

    public Sprite forest;
    public Sprite mountain;

    public GameObject child;

    SpriteRenderer SR;
    SpriteRenderer CSR;

    Transform cameraPos;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        SR = gameObject.GetComponent<SpriteRenderer>();
        CSR = child.GetComponent<SpriteRenderer>();
        instance = this;

        cameraPos = Camera.main.transform;
        lastPos = cameraPos.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 movement = cameraPos.position - lastPos;
        transform.position += new Vector3(movement.x * movementMulti.x, movement.y * movementMulti.y);
        lastPos = cameraPos.position;
    }

    public void Forest()
    {
        SR.sprite = forest;
        CSR.sprite = forest;
    }
    public void Mountain()
    {
        SR.sprite = mountain;
        CSR.sprite = mountain;
    }
}
