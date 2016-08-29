using UnityEngine;
using System.Collections;

public class SkeletonMotion : MonoBehaviour {
    NavMeshAgent myNav;
    Animator myAnim;

    public Transform waypoint;
	// Use this for initialization
	void Awake () {
        myNav = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
    }
    void Start()
    {
        myNav.destination = waypoint.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 localVelocity = transform.InverseTransformDirection(myNav.velocity);
        myAnim.SetFloat("VelX", localVelocity.x);
        myAnim.SetFloat("VelZ", localVelocity.z);
    }
}
