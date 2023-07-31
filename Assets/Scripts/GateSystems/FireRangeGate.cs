using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireRangeGate : BaseGate
{
    public TextMeshProUGUI BuffTitle;

    [SerializeField] float additionalRange;

    private void Start()
    {
        InitializeGateBuff();
    }
    public override float GiveGateBufferToGun()
    {
        return additionalRange;
    }

    public override void InitializeGateBuff()
    {
        if(gateType == GateTypes.PositiveRange) BuffTitle.text = "POSITIVE RANGE";
        else if (gateType == GateTypes.NegativeRange) BuffTitle.text = "NEGATIVE RANGE";

    }

    public override void OnCollisionWithBullet(int _)
    {
        return;
    }


}
