using UnityEngine;
using System.Collections;

public class DBSkeletonController : MonoBehaviour {
    Animator myAnimator;
	// Use this for initialization
	void Awake () {
        myAnimator = GetComponent<Animator>();

    }
	
	// Update is called once per frame

    void OnCollisionEnter(Collision collision)
    {
        myAnimator.SetTrigger("Hit");
    }
}
