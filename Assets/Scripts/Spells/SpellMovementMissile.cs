using UnityEngine;
using System.Collections;

public class SpellMovementMissile : MonoBehaviour, ISpellMovement {
    Vector3 velocity;
    Rigidbody myRigidbody;
    public float speed = 5f;
    public float maxLifeTime = 5f;
    private float deadline;
    // Use this for initialization
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
       
        if (Time.timeSinceLevelLoad > deadline)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void StartMovement(Vector3 worldVelocity)
    {
        velocity = worldVelocity.normalized;
        deadline = Time.timeSinceLevelLoad + maxLifeTime;
        myRigidbody.rotation.SetLookRotation(worldVelocity, Vector3.up);
        myRigidbody.AddForce(velocity.normalized * speed, ForceMode.Impulse);
    }
}
