using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject xShape;
    [SerializeField] GameObject oShape;
    [SerializeField] GameObject shapeGroup;
    [SerializeField] TMPro.TextMeshProUGUI message;
    [SerializeField] TMPro.TextMeshProUGUI turnStatus;

    private int turnCount = 0;
    private int currentTurn;
    public bool inGame;
    private GameGrid gameGrid;

    bool CompareTiles(int x, int y)
    {
        Debug.Log(gameGrid.grid[x, y].GetComponent<GridCell>().shapeNum);

        return gameGrid.grid[x, y].GetComponent<GridCell>().shapeNum == currentTurn;
    }

    public void DetermineGameState(GridCell gridCell)
    {
        GameObject shapeTemplate = currentTurn == 0 ? xShape : oShape;

        gridCell.Occupy(currentTurn, shapeTemplate, shapeGroup);

        bool isBoardFull = IsBoardFull();

        if (IsWinningPattern() || isBoardFull)
        {
            inGame = false;

            message.gameObject.SetActive(true);

            if (isBoardFull)
            {
                message.text = "Draw!";
            }
            else
            {
                message.text = currentTurn == 0 ? "X Won!" : "O Won!";
            }

            Invoke("StartMatch", 1);
        }
        else
        {
            EndTurn();
        }
    }

    bool IsWinningPattern()
    {
        if ((CompareTiles(0, 0) && CompareTiles(1, 0) && CompareTiles(2, 0)) ||
            (CompareTiles(0, 1) && CompareTiles(1, 1) && CompareTiles(2, 1)) ||
            (CompareTiles(0, 2) && CompareTiles(1, 2) && CompareTiles(2, 2)) ||
            (CompareTiles(0, 0) && CompareTiles(0, 1) && CompareTiles(0, 2)) ||
            (CompareTiles(1, 0) && CompareTiles(1, 1) && CompareTiles(1, 2)) ||
            (CompareTiles(2, 0) && CompareTiles(2, 1) && CompareTiles(2, 2)) ||
            (CompareTiles(0, 0) && CompareTiles(1, 1) && CompareTiles(2, 2)) ||
            (CompareTiles(0, 2) && CompareTiles(1, 1) && CompareTiles(2, 0)))
        {
            return true;
        }

        return false;
    }

    bool IsBoardFull()
    {
        int total = 0;

        for (int y = 0; y < gameGrid.gridHeight; y++)
        {
            for (int x = 0; x < gameGrid.gridWidth; x++)
            {
                if (gameGrid.grid[x, y].GetComponent<GridCell>().isOccupied)
                {
                    total++;
                }
            }
        }

        return total == gameGrid.gridWidth * gameGrid.gridHeight;
    }

    void EndTurn()
    {
        message.gameObject.SetActive(false);

        if (currentTurn == 0) currentTurn = 1;
        else currentTurn = 0;

        turnCount++;

        UpdateTurnStatus();
    }

    void UpdateTurnStatus ()
    {
        turnStatus.text = currentTurn == 0 ? "X" : "O";
    }

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();

        gameGrid.InitBoard();
        StartMatch();
    }

    void StartMatch()
    {
        currentTurn = 0;
        inGame = true;

        if (turnCount== 0)
        {
            message.text = "Click on any tile to start!";
            message.gameObject.SetActive(true);
        }
        else
        {
            message.gameObject.SetActive(false);
        }

        for (int y = 0; y < gameGrid.gridHeight; y++)
        {
            for (int x = 0; x < gameGrid.gridWidth; x++)
            {
                gameGrid.grid[x, y].GetComponent<GridCell>().Reset();
            }
        }

        foreach (Transform child in shapeGroup.transform)
        {
            Destroy(child.gameObject);
        }

        UpdateTurnStatus();
    }
}
