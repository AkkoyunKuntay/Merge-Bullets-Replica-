using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.ShootingIsOnEvent += OnShootingIsOn;
    }

    private void OnShootingIsOn()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
