using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IDamageable
{
    public float speed;
    public float range;
    [SerializeField] BulletData bulletData;

    [Header("Debug")]
    [SerializeField] bool isAllowedToMove;

   
    private void Start()
    {
        bulletData.pool = GetComponentInParent<Pooler>();

        range = PlayerController.instance.range;
       
        LevelManager.instance.BlockRaceIsOnEvent += OnBlockRaceIsOn;
        PlayerController.instance.GateBuffersTakenEvent += OnGateBuffersTaken;

        if (LevelManager.instance.gamePhases == GamePhases.Shooting) StartCoroutine(DestroyBulletByRange());

    }
    

    private void OnGateBuffersTaken()
    {
        range = PlayerController.instance.range;
      
           
    }

    private void OnBlockRaceIsOn()
    {
        isAllowedToMove = true;
    }

    private void Update()
    {
        if (LevelManager.instance.gamePhases == GamePhases.BlockRace)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
            
        if(LevelManager.instance.gamePhases == GamePhases.Shooting)
        {
            transform.position += transform.forward * speed * 2 * Time.deltaTime;

            
        }
    }

    IEnumerator DestroyBulletByRange()
    {
        yield return new WaitForSeconds(range);
        bulletData.pool.ReturnObject(gameObject);
    }

    public void TakeDamage(float damage)
    {
        bulletData.itemHP -= damage;
        if (bulletData.itemHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public BulletData GetBulletData()
    {
        return bulletData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BulletBarrier barrier))
        {
            Destroy(gameObject);
        }
        
        if(other.TryGetComponent(out BaseGate gate))
        {
            Debug.Log("Bullet Collision");
            gate.OnCollisionWithBullet((int)bulletData.itemDamage);
            bulletData.pool.ReturnObject(gameObject);
        }
    }
}

[System.Serializable]
public class BulletData
{
    public float itemHP;
    public float itemDamage;
    public Pooler pool;
}
