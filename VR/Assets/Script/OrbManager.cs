using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbManager : MonoBehaviour
{
    public static OrbManager instance { get; private set; }

    public List<GameObject> orbs;
    public List<Transform> spawnpoints;

    public Text display;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        score = 0;
        foreach(Transform sp in spawnpoints)
        {
            Instantiate(orbs[Random.Range(0, orbs.Count)], sp.position, sp.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Score()
    {
        score++;
        display.text = score.ToString();
    }
}
