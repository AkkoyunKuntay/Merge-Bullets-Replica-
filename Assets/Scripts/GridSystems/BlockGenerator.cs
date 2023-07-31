using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    [Space(5)]
    public int rowCount;
    public int columnCount;
    public float elementPadding;

    [Header("References")]
    [Space(5)]
    public Transform cellContainer;
    public GameObject cellPrefab;
    public GameObject[,] blocks;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        blocks = new GameObject[columnCount, rowCount];
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                blocks[i, j] = Instantiate(cellPrefab.gameObject, new Vector3(i * elementPadding, 0, j * elementPadding), Quaternion.identity);
                blocks[i, j].transform.parent = transform;
                blocks[i, j].transform.position = new Vector3(blocks[i, j].transform.position.x, blocks[i, j].transform.position.y + 0.5f,transform.position.z + j * elementPadding);
            }
        }
    }
}
