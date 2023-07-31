using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : CustomSingleton<GridGenerator>
{
    [Header("Grid Settings")]
    [Space(5)]
    public int rowCount;
    public int columnCount; 
    public float elementPadding;

    [Header("References")]
    [Space(5)]
    public Transform cellContainer;
    public GridCell cellPrefab;

    public List<GridCell> GridCells = new List<GridCell>();
    public GridCell[,] cells;

    private void Start()
    {
        GenerateGrid();
    }
    private void GenerateGrid()
    {
        cells = new GridCell[columnCount,rowCount];
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < rowCount; j++)
            {
                cells[i,j] = Instantiate(cellPrefab, new Vector3(i * elementPadding, 0,j * elementPadding), Quaternion.identity);
                cells[i,j].transform.parent = transform;
                cells[i,j].InitializeGridCell(i, j);

                if(!GridCells.Contains(cells[i, j]))
                {
                    GridCells.Add(cells[i, j]);
                }
                
            }
        }
    }
    public GridCell FindAvailableCell()
    {
        for (int i = 0; i < GridCells.Count; i++)
        {
            GridCell cell = GridCells[i];
            if (!cell.IsOccupied())
            {
                return cell;
            }
        }
        return null;
    }
    public Vector2Int GetCellCoordinatesByWorldPos(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / elementPadding);
        int y = Mathf.FloorToInt(worldPos.y / elementPadding);

        x = Mathf.Clamp(x, 0, columnCount);
        y = Mathf.Clamp (y, 0, rowCount);

        return new Vector2Int(x, y);
    }
    public Vector3 GetWorldPosByCellCoordinates(Vector2Int coordinates)
    {
        float x = coordinates.x * elementPadding;
        float y = coordinates.y * elementPadding;

        return new Vector3 (x,0,y);
    }
    
}
