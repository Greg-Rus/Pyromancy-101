using UnityEngine;
using System.Collections;

public class ParticleAtractor : MonoBehaviour {
    public ParticleSystem manaWell;
    public Bounds atractionVolume;
    public float destroyDistance = 0.01f;
    public float atractionVelocity = 0.5f;
    public float atractSlerpT = 0.5f;
    public float stabilizeSlerpT = 0.5f;
    public Color32 grippedColor;
    private Vector3 manaVelocity;

    ParticleSystem.Particle[] particles;
    // Use this for initialization
    void Awake()
    {
        particles = new ParticleSystem.Particle[manaWell.maxParticles];
        manaVelocity = new Vector3(0f, 0.05f, 0f);
    }

	//void Start () {
 //       atractionVolume.center = transform.position;
 //   }
	
	// Update is called once per frame
	void LateUpdate () {
        atractionVolume.center = transform.position;
        var count = manaWell.GetParticles(particles);
        for (int i = 0; i < count; ++i)
        {
            if (atractionVolume.Contains(particles[i].position))
            {
                Vector3 directionToHand = transform.position - particles[i].position;
                if (directionToHand.magnitude <= 0.1f)
                {
                    particles[i].lifetime = -1f;
                }
                else
                {
                    particles[i].startColor = grippedColor;
                    particles[i].velocity = Vector3.Slerp(particles[i].velocity,
                                                          directionToHand.normalized * atractionVelocity,
                                                          atractSlerpT);
                }
            }
            //else
            //{
            //    if (particles[i].velocity != manaVelocity)
            //    {
            //        Vector3 directionToHand = transform.position - particles[i].position;
            //        particles[i].velocity = Vector3.Slerp(particles[i].velocity, 
            //                                              manaVelocity,
            //                                              stabilizeSlerpT);
            //    }
            //}

        }
        manaWell.SetParticles(particles, count);
    }
}
