using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject coin;
    public GameObject potion;
    public bool dropItem;
    public bool dropPotion;
    bool broke;

    // Start is called before the first frame update
    void Start()
    {
        broke = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && broke == false)
        {
            broke = true;
            Destroy(gameObject, 0);
            if(dropItem)
            {
                if(dropPotion)
                {
                    Instantiate(potion, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(coin, transform.position, transform.rotation);
                }
            }
        }
    }
}
