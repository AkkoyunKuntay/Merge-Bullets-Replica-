using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNode : MonoBehaviour
{
    [SerializeField] bool hasGun;

    public void SetGun(Gun targetGun)
    {
        hasGun = true;
        targetGun.transform.SetParent(transform);
    }
    public bool HasGun()
    {
        return hasGun;
    }
}
