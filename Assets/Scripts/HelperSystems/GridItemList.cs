using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItemList : CustomSingleton<GridItemList>
{
    public List<GridItem> Items = new List<GridItem>();

    public int BuyItemCost;
    public GridItem GetGridItemByIndex(int index)
    {
        if(index >= Items.Count) return null;
        return Items[index];
    }

    public void BuyItem()
    {
        if(BuyItemCost<=ResourceManager.instance.GetResourceAmount())
        {
            ResourceManager.instance.PurchaseItem(BuyItemCost);
            GridCell cell = GridGenerator.instance.FindAvailableCell();
            if (cell != null)
            {
                cell.CreateNewItemByLevel(0);
            }
        }
    }
}
