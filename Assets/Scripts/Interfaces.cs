using UnityEngine;
using System.Collections;

public interface ISpellCharging
{
    void StartCharge();
    void ChargeSpell(float chargeProgress);
    void StopCharge();
}

public interface ISpellMovement
{
    void StartMovement(Vector3 worldVelocity);
}
