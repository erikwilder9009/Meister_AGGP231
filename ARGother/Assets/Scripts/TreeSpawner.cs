using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject trees;

    public float spawndelayLimit;
    float spawndelayTimer;
    float spawndelaySet;

    // Start is called before the first frame update
    void Start()
    {
        spawndelaySet = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawndelayTimer > spawndelaySet + spawndelayLimit)
        {
            GameObject tree = Instantiate(trees, transform.position, transform.rotation);
            Destroy(tree, 3);
            spawndelaySet = Time.time;
        }
        spawndelayTimer += 1 * Time.deltaTime;
    }
}
