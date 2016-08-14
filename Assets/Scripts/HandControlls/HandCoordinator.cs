using UnityEngine;
using System.Collections;

public class HandCoordinator : MonoBehaviour {
    public HandController leftHand;
    public HandController rightHand;
    public GameObject twoHandedFocusPoint;
    public float spellMergeDistance = 0.5f;

    Vector3 leftHandPosition;
    Vector3 rightHandPosition;
    bool spellMerging = false;
    Vector3 handGap;
    // Use this for initialization
    void Awake()
    {
        leftHand.myCoordinator = this;
        rightHand.myCoordinator = this;
    }

    void Start ()
    {
        twoHandedFocusPoint.transform.position = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckHandProximity();
	}

    void CheckHandProximity()
    {
        handGap = leftHand.transform.position - rightHand.transform.position;
        if (!spellMerging && handGap.magnitude < spellMergeDistance)
        {
            StartSpellMerge();
        }
        else if (spellMerging && handGap.magnitude < spellMergeDistance)
        {
            UpdateSpellMerge();
        }
        else if (spellMerging && handGap.magnitude > spellMergeDistance)
        {
            CancelSpellMerge();
        }
    }

    void StartSpellMerge()
    {
        spellMerging = true;
        twoHandedFocusPoint.SetActive(true);
    }

    void UpdateSpellMerge()
    {
        twoHandedFocusPoint.transform.position = rightHand.transform.position + handGap * 0.5f;
    }

    void CancelSpellMerge()
    {
        twoHandedFocusPoint.transform.position = Vector3.zero;
        twoHandedFocusPoint.SetActive(false);
    }
}
