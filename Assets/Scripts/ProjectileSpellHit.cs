using UnityEngine;
using System.Collections;

public class ProjectileSpellHit : MonoBehaviour {
    public GameObject hitEffect;
    public GameObject sphere;
    private AudioSource myAudio;
    public AudioClip[] flameCast;
    public AudioClip[] flameHit;
    private Rigidbody myRigidbody;

    void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        PlayFlameCastSound();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject instance = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal, transform.up)) as GameObject;
        PlayFlameHitSound();
        myRigidbody.isKinematic = true;
        sphere.GetComponent<MeshRenderer>().enabled = false;
        Destroy(instance, 2f);
        Destroy(this.gameObject,2);
    }

    void PlayFlameCastSound()
    {
        PlayRandomSound(flameCast);
    }

    void PlayFlameHitSound()
    {
        PlayRandomSound(flameHit);
    }

    void PlayRandomSound(AudioClip[] sounds)
    {
        int soundIndex = UnityEngine.Random.Range(0, sounds.Length);
        myAudio.PlayOneShot(sounds[soundIndex]);
    }
}
