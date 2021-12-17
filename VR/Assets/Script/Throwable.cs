using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public GameObject explosion;
    AudioSource AS;
    MeshRenderer MR;
    public List<AudioClip> sounds;
    ParticleSystem[] PSL;

    // Start is called before the first frame update
    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
        MR = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            var ploseion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject.GetComponent<TrailRenderer>());
            PSL = ploseion.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem PS in PSL)
            {
                var main = PS.main;
                main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
            AS.PlayOneShot(sounds[Random.Range(0, sounds.Count)]);
            Destroy(gameObject, 2f);
            Destroy(ploseion, 2f);
            OrbManager.instance.Score();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Target>())
        {
            var ploseion = Instantiate(explosion, transform.position, transform.rotation);
            PSL = ploseion.GetComponentsInChildren<ParticleSystem>();
            Destroy(gameObject.GetComponent<TrailRenderer>());
            foreach (ParticleSystem PS in PSL)
            {
                var main = PS.main;
                main.startColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
            AS.PlayOneShot(sounds[Random.Range(0, sounds.Count)]);
            Destroy(gameObject, 2f);
            Destroy(ploseion, 2f);
            OrbManager.instance.Score();
        }
    }
}
