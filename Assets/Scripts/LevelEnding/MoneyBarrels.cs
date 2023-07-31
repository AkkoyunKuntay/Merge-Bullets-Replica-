using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBarrels : MonoBehaviour,IDamageable
{
    public int barrelHealth;
    public TextMeshProUGUI healthText;

    public MoneyController moneyPrefab;

    private void Start()
    {
        healthText.text = barrelHealth.ToString();
       
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bullet bullet))
        {
            TakeDamage(bullet.GetBulletData().itemDamage);
            Destroy(bullet.gameObject);
        }
        if (other.TryGetComponent(out PlayerController Player))
        {
            GameManager.instance.EndGame(true);
        }
    }

    public void TakeDamage(float damage)
    {
        barrelHealth -= (int)damage;
        healthText.text = barrelHealth.ToString();
        if (barrelHealth <= 0)
        {
            MoneyController money =  Instantiate(moneyPrefab, transform.position, Quaternion.identity);            
            Destroy(gameObject);    
        }
    }
}
