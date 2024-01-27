using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] public GameObject gridCellPrefab;

    public int gridWidth = 3;
    public int gridHeight = 3;
    private float gridSpaceSize = 1f;
    public GameObject[,] grid;

    // Start is called before the first frame update

    public void InitBoard()
    {
        grid = new GameObject[gridHeight, gridWidth];

        if (gridCellPrefab == null)
        {
            Debug.LogError("Error: No grid cell prefab selected!");

            return;
        }

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                grid[x, y] = Instantiate(gridCellPrefab);
                grid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                grid[x, y].transform.position = new Vector3(x * gridSpaceSize, 0, -y * gridSpaceSize);
                grid[x, y].transform.SetParent(transform);
                grid[x, y].gameObject.name = "GridCell (x: " + x.ToString() + ", y: " + y.ToString() + ")";
            }
        }
    }

    Vector2Int GetGridPosFromWorld(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / gridSpaceSize);
        int y = Mathf.FloorToInt(worldPosition.y / gridSpaceSize);

        x = Mathf.Clamp(x, 0, gridWidth);
        y = Mathf.Clamp(y, 0, gridHeight);

        return new Vector2Int(x, y);
    }

    Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * gridSpaceSize;
        float y = gridPos.y * gridSpaceSize;

        return new Vector3(x, 0, y);
    }
}
