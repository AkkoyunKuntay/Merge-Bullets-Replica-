using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GridItem : MonoBehaviour, IDraggable
{
    [SerializeField] LevelData levelData;
    public Transform originalParent;
    
    private void Start()
    {
        InitializeItem();
        DragAndDrop.instance.DraggableSelectedEvent += OnDraggableSelected;
    }
    public void GoToOriginalNest()
    {
        GridCell cell = originalParent.GetComponent<GridCell>();
        cell.SetCurrentItem(this);
        PlayerPrefs.SetInt(cell.GetSaveName(), levelData.itemLevel);
        transform.SetParent(originalParent);
        transform.DOMove(cell.GetCenter(), 0.5f);

    }
    public LevelData GetLevelData()
    {
        return levelData;
    }
    private void InitializeItem()
    {
        transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero).SetEase(Ease.OutBounce);
        originalParent = transform.parent;
        GridCell cell = originalParent.GetComponent<GridCell>();
        cell.SetCurrentItem(this);
    }
    private void OnDraggableSelected(IDraggable obj)
    {
        if (obj != this as IDraggable) return;
        GridCell previousCell = originalParent.GetComponent<GridCell>();
        previousCell.SetCurrentItem(null);
        PlayerPrefs.SetInt(previousCell.GetSaveName(), -1);

        //originalParent = transform.parent;
        transform.SetParent(null); 

    }
    
}

[System.Serializable]
public class LevelData
{
    public int itemLevel;
    public Bullet bulletPrefab;
}