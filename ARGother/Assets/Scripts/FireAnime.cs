using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnime : MonoBehaviour
{
    public Sprite[] SpriAnimes = new Sprite[] {};
    SpriteRenderer spriteRenderer;





    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating("Animate", .5f, .75f);
    }

    // Update is called once per frame
    void Update() 
    {
    }

    void Animate()
    {
        int index = Random.Range(0, SpriAnimes.Length);
        spriteRenderer.sprite = SpriAnimes[index];
    }
}
