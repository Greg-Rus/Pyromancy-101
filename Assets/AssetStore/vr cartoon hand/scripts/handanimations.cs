using UnityEngine;
using System.Collections;

public enum HandAnimations { Idle, Point, GrabLarge, GrabSmall, GrabStickUp, GrabStickFront, ThumbUp, Fist, Gun, GunShoot, PushButton, Spread, MiddleFinger, Peace, OK};

public class handanimations : MonoBehaviour
{
    Animator anim;
    int Idle = Animator.StringToHash("Idle");
    int Point = Animator.StringToHash("Point");
    int GrabLarge = Animator.StringToHash("GrabLarge");
    int GrabSmall = Animator.StringToHash("GrabSmall");
    int GrabStickUp = Animator.StringToHash("GrabStickUp");
    int GrabStickFront = Animator.StringToHash("GrabStickFront");
    int ThumbUp = Animator.StringToHash("ThumbUp");
    int Fist = Animator.StringToHash("Fist");
    int Gun = Animator.StringToHash("Gun");
    int GunShoot = Animator.StringToHash("GunShoot");
    int PushButton = Animator.StringToHash("PushButton");
    int Spread = Animator.StringToHash("Spread");
    int MiddleFinger = Animator.StringToHash("MiddleFinger");
    int Peace = Animator.StringToHash("Peace");
    int OK = Animator.StringToHash("OK");


    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(HandAnimations Animation)
    {
        StopAllCoroutines();
        switch (Animation)
        {
            case HandAnimations.Fist: anim.SetTrigger(Idle); break;
            case HandAnimations.GrabLarge: anim.SetTrigger(GrabLarge); break;
            case HandAnimations.GrabSmall: anim.SetTrigger(GrabSmall); break;
            case HandAnimations.GrabStickFront: anim.SetTrigger(GrabStickFront); break;
            case HandAnimations.GrabStickUp: anim.SetTrigger(GrabStickUp); break;
            case HandAnimations.Gun: anim.SetTrigger(Gun); break;
            case HandAnimations.GunShoot: anim.SetTrigger(GunShoot); break;
            case HandAnimations.Idle: anim.SetTrigger(Idle); break;
            case HandAnimations.MiddleFinger: anim.SetTrigger(MiddleFinger); break;
            case HandAnimations.OK: anim.SetTrigger(OK); break;
            case HandAnimations.Peace: anim.SetTrigger(Peace); break;
            case HandAnimations.Point: anim.SetTrigger(Point); break;
            case HandAnimations.PushButton: anim.SetTrigger(PushButton); break;
            case HandAnimations.Spread: anim.SetTrigger(Spread); break;
            case HandAnimations.ThumbUp: anim.SetTrigger(ThumbUp); break;
            default: break;
        }
    }

    public void PlayAnimationAfterSeconds(HandAnimations animation, float seconds)
    {
        StopAllCoroutines();
        StartCoroutine(WaitAndPlayAnimation(animation, seconds));
    }
    IEnumerator WaitAndPlayAnimation(HandAnimations animation, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlayAnimation(animation);
    }
  
}