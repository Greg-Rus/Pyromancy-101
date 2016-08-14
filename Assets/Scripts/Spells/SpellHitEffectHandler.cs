using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpellSoundController))]
public class SpellHitEffectHandler : MonoBehaviour {
    SpellSoundController myAudio;
    ParticleSystem[] particleSystems;

    void Awake()
    {
        myAudio = GetComponent<SpellSoundController>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void OnEnable()
    {
        foreach (ParticleSystem system in particleSystems)
        {
            system.Play();
        }
        myAudio.PlayRandomSound();
        StartCoroutine(DespawnAfterTime(2f));
    }

    IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}