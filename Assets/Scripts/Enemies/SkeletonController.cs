using UnityEngine;
using System.Collections;

public class SkeletonController : MonoBehaviour
{
    Animator myAnimator;
    // Use this for initialization
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        myAnimator.SetTrigger("Hit");
    }

}