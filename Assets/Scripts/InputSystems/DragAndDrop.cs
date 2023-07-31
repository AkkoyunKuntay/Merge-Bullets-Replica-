using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DragAndDrop : CustomSingleton<DragAndDrop>
{
    [Header("Debug")]
    [SerializeField] GridItem currentDraggable;
    [SerializeField] LayerMask gridCellLayer;

    [SerializeField] bool isMouseDrag;
    private Vector3 screenPos;
    private Vector3 offset;

    public event System.Action<IDraggable> DraggableSelectedEvent;
    public event System.Action<GridItem> DragEvent;
    public event System.Action<IDroppable,GridItem> GridItemDroppedEvent;

    private void Update()
    {
        GridCell potentialCell = IsFingerOnGridCell();
        if (potentialCell != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (potentialCell.currentGridItem != null)
                {
                    GridItem selectedDraggable = potentialCell.currentGridItem;
                    currentDraggable = selectedDraggable;
                    isMouseDrag = true;

                    DraggableSelectedEvent?.Invoke(currentDraggable);
                    Debug.Log("DraggableSelected :" + currentDraggable);

                    // get screen position of dragging item.
                    screenPos = Camera.main.WorldToScreenPoint(currentDraggable.transform.position);
                    offset =
                        currentDraggable.transform.position - Camera.main.ScreenToWorldPoint(
                            new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z));
                }     
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(currentDraggable != null)
                {
                    isMouseDrag = false;
                    GridItemDroppedEvent?.Invoke(potentialCell, currentDraggable);
                    Debug.Log("Draggable Dropped to :" + potentialCell.name);
                    currentDraggable = null;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(currentDraggable != null)
                {
                    isMouseDrag = false;
                    currentDraggable.GoToOriginalNest();
                    currentDraggable = null;
                    
                }   
            }
        }

        if (isMouseDrag)
        {
            //track mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

            //It will update target gameobject's current postion.
            currentDraggable.transform.position = currentPosition;
        }

    }

    private GridCell IsFingerOnGridCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, gridCellLayer))
        {
            if (hit.transform.TryGetComponent(out GridCell cell))
                return cell;
        }
        return null;
    }
}
