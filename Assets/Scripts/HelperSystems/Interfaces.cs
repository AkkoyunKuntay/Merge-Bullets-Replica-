using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
  
}
public interface IDraggable
{
    public void GoToOriginalNest();
}
public interface IDroppable
{
    public Vector3 GetCenter();
    public bool IsOccupied();
    public bool IsMergeValid(int droppedItemLevel);
    public void CreateNewItemByLevel(int levenIndex);
    public void Merge(GridItem droppedItem);

}
public interface IDamageable
{
    public void TakeDamage(float damage);
}
