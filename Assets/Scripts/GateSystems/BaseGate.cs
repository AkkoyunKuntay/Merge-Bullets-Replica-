using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGate : MonoBehaviour
{
    public enum GateTypes {PositiveFireRate, NegativeFireRate,PositiveRange,NegativeRange, BulletSizeUp, TripleShot}
    public GateTypes gateType;

    public abstract void InitializeGateBuff();
    public abstract float GiveGateBufferToGun();
    public abstract void OnCollisionWithBullet(int value);

}
