using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    [SerializeField] int moneyValue;

    public void SetMoneyValue(int value)
    {
        moneyValue = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            ResourceManager.instance.IncreaseResourceCount(moneyValue);
            Destroy(gameObject);
        }
    }
}
