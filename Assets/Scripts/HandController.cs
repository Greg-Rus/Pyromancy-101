using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandController : MonoBehaviour {
    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;
    Transform parent;
    handanimations handAnimator;
    GameObject fireboltChargeInstance;
    GameObject projectileInstance;
    bool charged = false;
    public FireBoltChargeController chargeController;
    Vector3 lastHandPosition;


    public float spellCharge;
    public float stageOneCharge = 1f;
    public GameObject fireBolt;
    public GameObject fireBoltCharge;
    public Transform focusPoint;
    public float throwForceMultiplyer = 3f;

    // Use this for initialization
    void Awake () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        parent = (trackedObject.origin) ? trackedObject.origin : trackedObject.transform.parent;
        handAnimator = GetComponentInChildren<handanimations>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        PollInput();
	}

    private void PollInput()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            handAnimator.PlayAnimation(HandAnimations.GrabLarge);
            fireboltChargeInstance = Instantiate(fireBoltCharge, focusPoint.transform.position, Quaternion.identity) as GameObject;
            fireboltChargeInstance.transform.SetParent(focusPoint);
            chargeController = fireboltChargeInstance.GetComponent<FireBoltChargeController>();
            lastHandPosition = transform.position;
        }

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            spellCharge += (lastHandPosition - transform.position).magnitude;
            lastHandPosition = transform.position;
            if (spellCharge < stageOneCharge && !charged)
            {
                float strength = (spellCharge / stageOneCharge);
                chargeController.ScaleChargeEffect(strength);

            }
            if(spellCharge >= stageOneCharge && !charged)
            {
                StartCoroutine(LongVibration(0.5f, 0.5f));
                charged = true;
                Destroy(fireboltChargeInstance);
                projectileInstance = Instantiate(fireBolt, focusPoint.transform.position, Quaternion.identity) as GameObject;
                projectileInstance.transform.SetParent(focusPoint.transform);
            }
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (charged)
            {
                handAnimator.PlayAnimation(HandAnimations.Spread);
                handAnimator.PlayAnimationAfterSeconds(HandAnimations.Idle, 0.5f);
                chargeController = null;
                CastFireBolt();
                spellCharge = 0;
                charged = false;
            }
            else
            {
                handAnimator.PlayAnimation(HandAnimations.Idle);
                Destroy(fireboltChargeInstance, 0.2f);
                chargeController = null;
                spellCharge = 0;
            }
            
        }
    }

    void CastFireBolt()
    {
        projectileInstance.transform.SetParent(null);
        Rigidbody projectileRigidbody = projectileInstance.GetComponentInChildren<Rigidbody>();
        projectileRigidbody.isKinematic = false;
        StartCoroutine(CastRigidbodySpell(projectileRigidbody));
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)device.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
    IEnumerator CastRigidbodySpell(Rigidbody rigidbody)
    {
        yield return new WaitForFixedUpdate();
        Vector3 worldVelocity = parent.TransformVector(device.velocity);
        rigidbody.velocity = worldVelocity * throwForceMultiplyer;

        
    }

}
