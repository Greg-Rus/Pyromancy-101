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

    void Update()
    {
        sphere.transform.Rotate(Vector3.up, 1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject instance = Instantiate(hitEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal, transform.up)) as GameObject;
        PlayFlameHitSound();
        myRigidbody.isKinematic = true;
        //sphere.GetComponent<MeshRenderer>().enabled = false;
        sphere.SetActive(false);
        Destroy(instance, 2f);
        Destroy(this.gameObject,2);
    }

    public void Cast(Vector3 velocity)
    {
        transform.SetParent(null);
        PlayFlameCastSound();
        myRigidbody.isKinematic = false;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        StartCoroutine(ApplyVelocityInNextFixedUpdate(velocity));
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

    IEnumerator ApplyVelocityInNextFixedUpdate(Vector3 velocity)
    {
        yield return new WaitForFixedUpdate();
        myRigidbody.velocity = velocity;
    }
}
