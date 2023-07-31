using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] Pooler bulletPool; 

    [SerializeField] Bullet bulletPrefab;
    [SerializeField] bool hasBullet;
    [SerializeField] bool allowFire;

    public float fireRate;
    private float fireTime;

    private void Start()
    {
        LevelManager.instance.ShootingIsOnEvent += OnShootingIsOn;
        PlayerController.instance.GateBuffersTakenEvent += OnGateBuffersTaken;
    }

    private void OnGateBuffersTaken()
    {
        
        fireRate = PlayerController.instance.fireRate;
        if( fireRate < 0 ) { fireRate = 0.025f; }
        fireTime = fireRate;
    }
    private void OnShootingIsOn()
    {
        allowFire = true;
        if(hasBullet) transform.position = transform.parent.position;

    }

    private void Update()
    {
        if (!GameManager.instance.isLevelActive) return;
        if (!allowFire) return;
        if(!hasBullet) return;
        fireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0))
        {     
            if(fireTime <= 0)
            {
                GameObject bulleObj = bulletPool.GetObject();
                bulleObj.transform.position = transform.position;
                bulleObj.transform.rotation = Quaternion.identity;
                bulleObj.SetActive(true);
                fireTime = fireRate;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            LevelManager.instance.SetGamePhase(GamePhases.Shooting);
        }

        if (other.TryGetComponent(out GridItem gridItem))
        {
            
            if (!hasBullet)
            {
                hasBullet = true;
                bulletPrefab = gridItem.GetLevelData().bulletPrefab;
                bulletPool = PoolManager.instance.GetPoolerByItemLevel(gridItem.GetLevelData().itemLevel);
                PlayerNode node = PlayerController.instance.FindAvailableNode();
                if (node != null)
                {
                    node.SetGun(this);
                    transform.DOMove(transform.parent.position, 0.5f);
                    PlayerController.instance.RegisterGun(this);
                }

                Destroy(gridItem.gameObject);
            }
            
        }

    }
}
