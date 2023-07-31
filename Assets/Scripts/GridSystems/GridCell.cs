using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour,IDroppable
{
    public GridItem currentGridItem;
       
    [Header("Debug")]
    [SerializeField] int posX;
    [SerializeField] int posY;
    [SerializeField] bool isOccupied = false;

    string saveName;


    private void Start()
    {
        int itemLevel = PlayerPrefs.GetInt(saveName, -1);
        if(itemLevel >= 0)
        {
            CreateNewItemByLevel(itemLevel);
        }

        DragAndDrop.instance.GridItemDroppedEvent += OnItemDropped;
    }

    private void OnItemDropped(IDroppable gridCell,GridItem droppedItem)
    {
        if (gridCell != this as IDroppable) return;

        if (!isOccupied)
        {
            SetCurrentItem(droppedItem);
            droppedItem.transform.SetParent(transform);
            droppedItem.transform.position = GetCenter();
            PlayerPrefs.SetInt(saveName, droppedItem.GetLevelData().itemLevel);
        }
        else
        {
            if (IsMergeValid(droppedItem.GetLevelData().itemLevel))
            {
                Debug.Log("MERGE !!!");
                Merge(droppedItem);
            }
            else
            {
                droppedItem.GoToOriginalNest();
            }
        }
    }
    public void InitializeGridCell(float x,float y)
    {
        posX = (int)x;
        posY = (int)y;
        gameObject.name = "Cell " + "(" + posX + "," + posY + ")";
        saveName = gameObject.name;
    }
    public void SetCurrentItem(GridItem desiredItem)
    {      
         
        if (desiredItem != null)
        {
            currentGridItem = desiredItem;
            isOccupied = true;
        }      
        else
        {
            currentGridItem = null;
            isOccupied = false;
        }
            
        IsOccupied();

    }
    public Vector2Int GetCellPosition()
    { 
        return new Vector2Int(posX, posY);
    }
    public Vector3 GetCenter()
    {
        return transform.position;
    }
    public void CreateNewItemByLevel(int levelIndex)
    {
        Debug.Log("New Item Created with level" + levelIndex);
        GridItem newItem = GridItemList.instance.GetGridItemByIndex(levelIndex);
        //Instantiate Item
        Instantiate(newItem,GetCenter(),Quaternion.identity,transform);
        
        //SetCurrentItem();
        SetCurrentItem(newItem);
        PlayerPrefs.SetInt(saveName, newItem.GetLevelData().itemLevel);
    }
    public bool IsOccupied()
    {
        isOccupied = currentGridItem != null ? true: false;
        return isOccupied;
    }
    public bool IsMergeValid(int droppedItemLevel)
    {
        bool canMerge = droppedItemLevel == currentGridItem.GetLevelData().itemLevel &&
                        droppedItemLevel < GridItemList.instance.Items.Count-1
                        ? true : false;

        return canMerge;
    }
    public void Merge(GridItem droppedItem)
    {
        int targetLevel = currentGridItem.GetLevelData().itemLevel+1;
        
        Destroy(currentGridItem.gameObject);
        Destroy(droppedItem.gameObject);
        SetCurrentItem(null);
        CreateNewItemByLevel(targetLevel);
        
    }
    public string GetSaveName()
    {
        return saveName;
    }
   
}
