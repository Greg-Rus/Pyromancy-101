using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandController : MonoBehaviour {
    SteamVR_Controller.Device device;
    Transform parent;

    handanimations handAnimator;
    Vector3 lastHandPosition;

    Vector3 oldDirection;
    Vector3 startPosition;
    public float acceptableAngleChange = 15f;
    Vector3 smotthedDirection;

    public HandCoordinator myCoordinator;
    public SpellChargeController spellChargeController;
    public ControllerDeviceHandler deviceHandler;
    public SpellCastController spellCastController;
    public LineRenderer lineRenderer;
    public Transform head;
    public LayerMask aimAssistLayers;

    // Use this for initialization
    void Awake () { 
        handAnimator = GetComponentInChildren<handanimations>();
    }
    void Start()
    {
        parent = deviceHandler.GetParentTransform();
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
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            SceneManager.LoadScene(0);
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
        //lineRenderer.SetPosition(0, head.transform.position);
        //lineRenderer.SetPosition(1, head.transform.position + head.transform.forward * 100f);
        spellChargeController.ChargeSpell((lastHandPosition - transform.position).magnitude);
        lastHandPosition = transform.position;        
    }

    void ReleaseCharge()
    {
        if (spellChargeController.charged)
        {
            handAnimator.PlayAnimation(HandAnimations.Spread);
            handAnimator.PlayAnimationAfterSeconds(HandAnimations.Idle, 0.5f);
            if (spellChargeController.spellChargeLevel == SpellCharge.FireTwo)
            {
                spellCastController.CastSpell(spellChargeController.spellChargeLevel, GetAimAssistedDirection());
            }
            else
            {
                spellCastController.CastSpell(spellChargeController.spellChargeLevel, parent.TransformVector(device.velocity));
            }
            
            spellChargeController.Reset();
        }
        else
        {
            handAnimator.PlayAnimation(HandAnimations.Idle);
            spellChargeController.Reset();
        }
    }

    Vector3 GetAimAssistedDirection()
    {
        Vector3 deviceDirection = parent.TransformVector(device.velocity);
        RaycastHit hit;
        Vector3 target;
        Vector3 assistedDirection;
        //RenderVolume(head.transform.position + head.transform.up * 1f,
        //             head.transform.position - head.transform.up * 1f,
        //             0.5f, head.transform.forward, 100f);
        
        if (Physics.CapsuleCast(head.transform.position + head.transform.up * 1f,
                                head.transform.position - head.transform.up * 1f,
                                0.5f, head.transform.forward, out hit, 100f, aimAssistLayers))
        {
            target = hit.point;
            Vector3 idealDirection = target - transform.position;
            float angleDelta = Vector3.Angle(idealDirection, deviceDirection);
            if (angleDelta < acceptableAngleChange)
            {
                assistedDirection = idealDirection.normalized * deviceDirection.magnitude;
            }
            else
            {
                assistedDirection = parent.TransformVector(device.velocity);
            }
        }
        else
        {
            assistedDirection = parent.TransformVector(device.velocity);
        }

        return assistedDirection;
    }

    private void SpellChargeReached(SpellCharge spellChargeLevel)
    {
        switch (spellChargeLevel)
        {
            case SpellCharge.FireOne: deviceHandler.VibrateController(0.2f, 0.1f); break;
            case SpellCharge.FireTwo: deviceHandler.VibrateController(0.2f, 0.2f); break;
            case SpellCharge.FireThree: deviceHandler.VibrateController(0.2f, 0.3f); break;
        }
    }

    //------------------------To Visualize Aim Assist Hit Volume-----------------------------------

    void RenderVolume(Vector3 p1, Vector3 p2, float radius, Vector3 dir, float distance)
    {
        if (!shape)
        { // if shape doesn't exist yet, create it
            shape = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
            Destroy(shape.GetComponent<Collider>()); // no collider, please!
            shape.GetComponent<Renderer>().material = mat; // assign the selected material to it
        }
        Vector3 scale; // calculate desired scale
        float diam = 2 * radius; // calculate capsule diameter
        scale.x = diam; // width = capsule diameter
        scale.y = Vector3.Distance(p2, p1) + diam; // capsule height
        scale.z = distance + diam; // volume length
        shape.localScale = scale; // set the rectangular volume size
                                  // set volume position and rotation
        shape.position = (p1 + p2 + dir.normalized * distance) / 2;
        shape.rotation = Quaternion.LookRotation(dir, p2 - p1);
        shape.GetComponent<Renderer>().enabled = true; // show it
    }

    private Transform shape;
    public float range = 10; // range of the capsule cast
    private float freeDistance = 0;
    public Material mat;

}
