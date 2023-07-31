using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;

    [SerializeField] int poolSize;
    [SerializeField] bool expandable;

    private List<GameObject> freeList;
    private List<GameObject> usedList;

    
    private void Awake()
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GenerateNewObject();
        }   
    }

    public GameObject GetObject()
    {
        int totalFree = freeList.Count;
        if (totalFree == 0 && !expandable) return null;
        else if (totalFree == 0) GenerateNewObject();

        GameObject go = freeList[0];
        freeList.RemoveAt(0);
        usedList.Add(go);
        return go;
    }

    public void ReturnObject(GameObject obj)
    {
        Debug.Assert(usedList.Contains(obj));
        obj.SetActive(false);
        usedList.Remove(obj);
        freeList.Add(obj);
    }

    private void GenerateNewObject()
    {
        GameObject go =  Instantiate(itemPrefab);
        go.transform.parent = transform;
        go.SetActive(false);
        freeList.Add(go);

    }
}
