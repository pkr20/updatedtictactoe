using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private LayerMask gridLayer;

    GameGrid gameGrid;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        GridCell cellIsMouseOver = IsMouseOverAGridSpace();

        if (cellIsMouseOver != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!cellIsMouseOver.isOccupied && gameController.inGame)
                {
                    gameController.DetermineGameState(cellIsMouseOver);
                }
            }
        }
    }

    private GridCell IsMouseOverAGridSpace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, gridLayer))
        {
            return hitInfo.transform.GetComponent<GridCell>();
        }
        else
        {
            return null;
        }
    }
}
