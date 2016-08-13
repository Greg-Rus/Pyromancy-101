using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandController : MonoBehaviour {
    SteamVR_Controller.Device device;
    Transform parent;

    handanimations handAnimator;
    Vector3 lastHandPosition;

    Vector3 oldDirection;
    Vector3 startPosition;
    float acceptableAngleChange = 25f;
    Vector3 smotthedDirection;

    public HandCoordinator myCoordinator;
    public SpellChargeController spellChargeController;
    public ControllerDeviceHandler deviceHandler;
    public SpellCastController spellCastController;
    public LineRenderer lineRenderer;
    public Transform head;

    // Use this for initialization
    void Awake () {
        parent = deviceHandler.GetParentTransform();
        handAnimator = GetComponentInChildren<handanimations>();
        spellChargeController.OnSpellCharge += SpellChargeReached;
    }
	
	void Update ()
    {
        PollInput();
	}

    private void PollInput()
    {
        device = deviceHandler.GetDeviceState(); ;
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            StartSpellCharge();
        }

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            UpdateCharge();
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ReleaseCharge();
        }
    }

    void StartSpellCharge()
    {
        handAnimator.PlayAnimation(HandAnimations.GrabLarge);
        spellChargeController.StartCharge();
        lastHandPosition = transform.position;

        oldDirection = parent.TransformVector(device.velocity).normalized;
        startPosition = transform.position;
    }

    void UpdateCharge()
    {
        spellChargeController.ChargeSpell((lastHandPosition - transform.position).magnitude);
        lastHandPosition = transform.position;
        DrawTestLine();
        
    }

    void DrawTestLine()
    {
        Vector3 newDirection = parent.TransformVector(device.velocity);
        Vector3 lookAtVector = parent.transform.forward;
        RaycastHit hit;
        Vector3 target;

        //if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, 100f))
        //{
        //    target = hit.point;
        //}
        //else
        //{
        //    target = parent.transform.forward * 100f;
        //}
        if (Physics.CapsuleCast(head.transform.position + head.transform.up * 1.5f,
                                head.transform.position - head.transform.up * 1.5f,
                                0.5f, head.transform.forward, out hit, 10))
        {
            target = hit.point;
            Vector3 idealDirection = target - transform.position;
            float angleDelta = Vector3.Angle(idealDirection, newDirection);
            Debug.Log(angleDelta);
            if (angleDelta < acceptableAngleChange)
            {
                smotthedDirection = idealDirection;
            }
        }
        else
        {
            smotthedDirection = parent.TransformVector(device.velocity);
        }




        //Vector3 idealDirection = target - transform.position;

        //float angleDelta = Vector3.Angle(idealDirection, newDirection);
        //Debug.Log(angleDelta);
        //if (angleDelta < acceptableAngleChange)
        //{
        //    smotthedDirection = idealDirection;
        //}
        //else
        //{
        //    smotthedDirection = parent.TransformVector(device.velocity);
        //}
        lineRenderer.SetPosition(0, head.transform.position);
        lineRenderer.SetPosition(1, head.transform.position + head.transform.forward * 20f);
        //lineRenderer.SetPosition(0, transform.position);
        //lineRenderer.SetPosition(1, transform.position + smotthedDirection * parent.TransformVector(device.velocity).magnitude);

    }

    void ReleaseCharge()
    {
        if (spellChargeController.charged)
        {
            handAnimator.PlayAnimation(HandAnimations.Spread);
            handAnimator.PlayAnimationAfterSeconds(HandAnimations.Idle, 0.5f);
            spellCastController.CastSpell(spellChargeController.spellChargeLevel, smotthedDirection * parent.TransformVector(device.velocity).magnitude); //parent.TransformVector(device.velocity));
            spellChargeController.Reset();
        }
        else
        {
            handAnimator.PlayAnimation(HandAnimations.Idle);
            spellChargeController.Reset();
        }
    }

    private void SpellChargeReached(SpellCharge spellChargeLevel)
    {
        switch (spellChargeLevel)
        {
            case SpellCharge.FireOne: deviceHandler.VibrateController(0.2f, 0.2f); break;
            case SpellCharge.FireTwo: deviceHandler.VibrateController(0.2f, 0.4f); break;
        }
    }

}
