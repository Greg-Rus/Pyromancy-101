using UnityEngine;
using System.Collections;

public class ControllerDeviceHandler : MonoBehaviour {
    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;


    // Use this for initialization
    void Awake () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    public Transform GetParentTransform()
    {
        return (trackedObject.origin) ? trackedObject.origin : trackedObject.transform.parent;
    }

    public SteamVR_Controller.Device GetDeviceState()
    {
        return SteamVR_Controller.Input((int)trackedObject.index);
    }

    public void VibrateController(float length, float strength)
    {
        StartCoroutine(LongVibration(length, strength));
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)trackedObject.index).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }

}
