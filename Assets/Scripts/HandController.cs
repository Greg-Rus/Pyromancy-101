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
    public FireBoltChargeController chargeParticleController;
    Vector3 lastHandPosition;

    public HandCoordinator myCoordinator;

    public float spellCharge;
    public float stageOneCharge = 1f;
    public float stageTwoCharge = 2.5f;
    public GameObject fireBolt;
    public ParticleSystem fireBoltCharge;
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
            StartSpellCharge();
        }

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            UpdateCharge();
            CheckChargeStage();
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ReleaseCharge();
           
            
        }
    }

    void StartSpellCharge()
    {
        handAnimator.PlayAnimation(HandAnimations.GrabLarge);
        chargeParticleController.StartCharge();
    }

    void UpdateCharge()
    {
        spellCharge += (lastHandPosition - transform.position).magnitude;
        lastHandPosition = transform.position;
    }

    void CheckChargeStage()
    {
        if (spellCharge < stageOneCharge && !charged)
        {
            float strength = (spellCharge / stageOneCharge);
            chargeParticleController.ScaleChargeEffect(strength);
        }
        if (spellCharge >= stageOneCharge && !charged)
        {
            charged = true;
            chargeParticleController.StopCharge();
            StartCoroutine(LongVibration(0.2f, 0.3f));
            SpawnSpell();
        }
    }

    void SpawnSpell()
    {
        projectileInstance = Instantiate(fireBolt, focusPoint.transform.position, Quaternion.identity) as GameObject;
        projectileInstance.transform.SetParent(focusPoint.transform);
    }

    void ReleaseCharge()
    {
        if (charged)
        {
            handAnimator.PlayAnimation(HandAnimations.Spread);
            handAnimator.PlayAnimationAfterSeconds(HandAnimations.Idle, 0.5f);
            chargeParticleController.StopCharge();
            CastFireBolt();
            spellCharge = 0;
            charged = false;
        }
        else
        {
            handAnimator.PlayAnimation(HandAnimations.Idle);
            chargeParticleController.StopCharge();
            spellCharge = 0;
        }
    }

    void CastFireBolt()
    {
        Vector3 worldVelocity = parent.TransformVector(device.velocity);
        projectileInstance.GetComponent<ProjectileSpellHit>().Cast(worldVelocity * throwForceMultiplyer);
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)device.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }

}
