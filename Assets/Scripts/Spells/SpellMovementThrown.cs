using UnityEngine;
using System.Collections;

public class SpellMovementThrown : MonoBehaviour, ISpellMovement {
    Rigidbody myRigidbody;
    public float velocityMultiplyer;
	// Use this for initialization
	void Awake ()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void StartMovement(Vector3 velocity)
    {
        myRigidbody.velocity = velocity * velocityMultiplyer;
    }
}
