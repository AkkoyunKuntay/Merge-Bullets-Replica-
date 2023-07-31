using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour,IDamageable
{
    public float blockHP;
    public float blockDamage;

    public void TakeDamage(float damage)
    {
        blockHP -= damage;
        if (blockHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            Debug.Log("Bullet Hit!");
            bullet.TakeDamage(blockDamage);
            TakeDamage(bullet.GetBulletData().itemDamage);

        }
    }
}
