using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FireRateGate : BaseGate
{
    public TextMeshProUGUI BuffTitle;
    public TextMeshProUGUI buffText;
    [SerializeField] int minValue,maxValue;
    [SerializeField] int selectedValue;

    private void Start()
    {
        InitializeGateBuff();
    }

    public override void InitializeGateBuff()
    {
        selectedValue = Random.Range(minValue,maxValue);
        buffText.text = selectedValue.ToString();
        BuffTitle.text = "FIRE RATE";
    }

    public override void OnCollisionWithBullet(int value)
    {
        selectedValue += value;
        buffText.text = selectedValue.ToString();
    }

    public override float GiveGateBufferToGun()
    {
        return (float)selectedValue;
    }
}
