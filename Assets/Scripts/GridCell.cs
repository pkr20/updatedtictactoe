using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public int posX;
    public int posY;
    public int shapeNum = 2;
    public bool isOccupied = false;
    public GameObject objectInThisGridSpace = null;

    public void Occupy(int shapeNumber, GameObject shapeObject, GameObject shapeGroup)
    {
        GameObject shape = Instantiate(shapeObject);
        shape.transform.SetParent(shapeGroup.transform);
        shape.transform.position = new Vector3(posX, 0, -posY);

        shapeNum = shapeNumber;
        objectInThisGridSpace = shapeObject;
        isOccupied = true;
    }

    public void Reset()
    {
        shapeNum = 2;
        objectInThisGridSpace = null;
        isOccupied = false;
    }

    public void SetPosition (int x, int y)
    {
        posX = x;
        posY = y;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }
}
