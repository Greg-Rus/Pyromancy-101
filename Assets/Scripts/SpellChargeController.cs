using UnityEngine;
using System.Collections;

public class SpellChargeController : MonoBehaviour
{
    public GameObject stageOneCharge;
    public GameObject stageTwoCharge;

    public float stageOneChargeTarget = 1f;
    public float stageTwoChargeTarget = 2.5f;

    public bool charged = false;
    public float spellChargeAmount;
    public SpellCharge spellChargeLevel = SpellCharge.None;

    public delegate void OnSpellCharged(SpellCharge spellChargeLevel);
    public OnSpellCharged OnSpellCharge;

    private float currentChargeGoal = 0;
    private ISpellCharging stageOneChargeEffect;
    private ISpellCharging stageTwoChargeEffect;
    private SpellCharge nextSpellChargeLevel;

    private ISpellCharging currentSpellChargeEffect = null;
    // Use this for initialization
    void Awake ()
    {
        stageOneChargeEffect = stageOneCharge.GetComponent<ISpellCharging>() as ISpellCharging;
        stageTwoChargeEffect = stageTwoCharge.GetComponent<ISpellCharging>() as ISpellCharging;
    }
	
    public void StartCharge()
    {
        SetTargetCharge(spellChargeLevel);
    }
    public void ChargeSpell(float chargeProgress)
    {
        if (spellChargeLevel != SpellCharge.FireTwo)
        {
            spellChargeAmount += chargeProgress;
            CheckChargeStage();
        }
        
    }
    public void Reset()
    {
        spellChargeAmount = 0f;
        charged = false;
        spellChargeLevel = SpellCharge.None;
        currentSpellChargeEffect.StopCharge();
    }

    void CheckChargeStage()
    {
        if (spellChargeAmount < currentChargeGoal && !charged)
        {
            float strength = (spellChargeAmount / stageOneChargeTarget);
            currentSpellChargeEffect.ChargeSpell(strength);
        }
        if (spellChargeAmount >= currentChargeGoal)
        {
            if (!charged)
            {
                charged = true;
            }
            currentSpellChargeEffect.StopCharge();
            spellChargeLevel = nextSpellChargeLevel;
            SetTargetCharge(spellChargeLevel);
            OnSpellCharge(spellChargeLevel);
        }
    }

    private void SetTargetCharge(SpellCharge currentSpellChargeLevel)
    {
        switch (currentSpellChargeLevel)
        {
            case SpellCharge.None:
                currentSpellChargeEffect = stageOneChargeEffect;
                currentChargeGoal = stageOneChargeTarget;
                nextSpellChargeLevel = SpellCharge.FireOne;
                break;
            case SpellCharge.FireOne:
                currentSpellChargeLevel = SpellCharge.FireOne;
                currentSpellChargeEffect = stageTwoChargeEffect;
                currentChargeGoal = stageTwoChargeTarget;
                nextSpellChargeLevel = SpellCharge.FireTwo;
                break;
            case SpellCharge.FireTwo:
                currentSpellChargeLevel = SpellCharge.FireTwo;
                break;
            default:
                Debug.LogError("No case for SetTargetCharge(" + currentSpellChargeLevel + ")");
                break;
        }

        currentSpellChargeEffect.StartCharge();
    }
}
