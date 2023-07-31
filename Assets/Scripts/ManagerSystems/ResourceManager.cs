using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : CustomSingleton<ResourceManager>
{
    [SerializeField] int moneyCount;

    public event System.Action ResourceChangedEvent;

    protected override void Awake()
    {
        base.Awake();
        moneyCount = PlayerPrefs.GetInt("money", 100);
    }
    public int GetResourceAmount()
    {   
        return moneyCount;
    }
    public void PurchaseItem(int cost)
    {
        moneyCount -= cost;
        ResourceChangedEvent?.Invoke();
        PlayerPrefs.SetInt("money", moneyCount);
    }
    public void IncreaseResourceCount(int count)
    {
        moneyCount += count;
        ResourceChangedEvent?.Invoke();
        PlayerPrefs.SetInt("money", moneyCount);
    }
}
