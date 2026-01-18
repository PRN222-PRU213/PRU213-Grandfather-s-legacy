using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Shape")]
public class ItemShape : ScriptableObject
{
    public int width;
    public int height;

    [Tooltip("1 = occupied, 0 = empty")]
    public int[] cells;

    public bool Occupies(int x, int y)
    {
        return cells[y * width + x] == 1;
    }

    public int GetTotalCells()
    {
        int count = 0;
        foreach (var cell in cells)
        {
            if (cell == 1) count++;
        }
        return count;
    }
}
