using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SpellSoundController : MonoBehaviour {
    private AudioSource myAudio;
    public AudioClip[] sounds;
    // Use this for initialization
    void Awake () {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        int soundIndex = UnityEngine.Random.Range(0, sounds.Length);
        myAudio.PlayOneShot(sounds[soundIndex]);
    }

}
