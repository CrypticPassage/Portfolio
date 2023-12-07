using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Cell CellPrefab;
    private Cell[,] Cells = null;
    public float CellSize = 1;
    public Transform CellsParent;
    public Text InputX;
    public Text InputY;
    public Text InputBombs;
    public Text CheckValuesText;
    public Text YouWin;
    public Text YouLose;
    private Vector2 FieldSize = new Vector2(15, 10);
    private int BombAmount = 15;
    private List<Cell> OpenedCells = null;
    private bool Gameended = false;
    private bool CorrectValues = true;
    private int OpenedCellCount = 0;
    private int OpenedFC = 0;

    public enum cellstate
    {
        closed = 1,
        opened = 2,
        spotted = 3,
        exploded = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckValuesText.text = "";
        YouWin.text = "";
        YouLose.text = "";
    }
  
    // Update is called once per frame
    void Update()
    {
        //Input.touches[0].fingerId
        //Touch[] touches = Input.touches;
        //Touch currTuch = touches[0];
        //currTuch.fingerId

        if (Input.GetMouseButtonUp(0))
        {
            if (!Gameended)
                LeftClick();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (!Gameended)
                RightClick();
        }
    }

    void LeftClick()
    {
        OnCellLeftClick(GetClickedCell());
        CheckForWin();
    }

    void RightClick()
    {
        Cell currentCell = GetClickedCell();
        if (currentCell)
        {
            OnCellRightClick(currentCell);
        }
    }

    Cell GetClickedCell()
    {
        if (Cells == null)
            return null;

        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell currentCell = Cells[x, y];
                if (currentCell == null)
                    continue;
                if (currentCell.coll != null)
                {
                    Vector2 min = currentCell.coll.bounds.min;
                    Vector2 max = currentCell.coll.bounds.max;
                    Vector2 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (click.x > min.x && click.x < max.x && click.y > min.y && click.y < max.y)
                    {
                        return currentCell;
                    }
                }
            }
        }
        return null;
    }

    void OnCellLeftClick(Cell currentCell)
    {
        if (currentCell == null)
            return;

        if (currentCell.GetState() == cellstate.closed)
        {
            if (currentCell.IsBomb)
            {
                currentCell.SetState(cellstate.exploded);
                Gameended = true;
                for (int x = 0; x < FieldSize.x; x++)
                {
                    for (int y = 0; y < FieldSize.y; y++)
                    {
                        Cell enumerationcell = Cells[x, y];
                        OpenCell(enumerationcell);
                        if (enumerationcell.IsBomb)
                            enumerationcell.SetState(cellstate.exploded);
                        if (enumerationcell.GetState() == cellstate.closed)
                            enumerationcell.SetState(cellstate.opened);
                    }
                }
                YouLose.text = "YOU LOSE!";
            }
            else
            {
                OpenCell(currentCell);
            }
        }
    }

    void OnCellRightClick(Cell currentCell)
    {
        if (currentCell.GetState() == cellstate.closed)
        {
            currentCell.SetState(cellstate.spotted);
        }
        else if (currentCell.GetState() == cellstate.spotted)
        {
            currentCell.SetState(cellstate.closed);
        }
    }

    void CheckForWin()
    {
        int square = (int)FieldSize.x * (int)FieldSize.y;
        // Debug.Log(openedcellscount);
        if (OpenedCellCount == square - BombAmount)
        {
            
            for (int x = 0; x < FieldSize.x; x++)
            {
                for (int y = 0; y < FieldSize.y; y++)
                {
                    Cell currentCell = Cells[x, y];
                        if (currentCell.IsBomb)
                        currentCell.SetState(cellstate.exploded);

                    if (currentCell.GetState() == cellstate.closed)
                        currentCell.SetState(cellstate.opened);
                }    
            }
            YouWin.text = "YOU WIN!";
            Gameended = true;
        }
    }

    void OpenCell(Cell cell)
    {
        if (OpenedCells.Contains(cell))
            return;
        OpenedCells.Add(cell); // Чтобы избежать зацикливания

        cell.SetState(cellstate.opened);
        OpenedCellCount++;
        OpenedFC++;

        if (OpenedFC == 1)
        CreateBombs(BombAmount);

        int bombaround = 0;
        List<Cell> neighborcells = GetNeighborCells(cell);
        for (int i = 0; i < neighborcells.Count; i++)
        {
            if (neighborcells[i].IsBomb)
                bombaround++;
        }

        if (bombaround > 0)
        {
            cell.text.text = bombaround.ToString();
        }
        else
        {
            cell.text.text = "";
            for (int i = 0; i < neighborcells.Count; i++)
            {
                OpenCell(neighborcells[i]);
            }
        }
    }

    List<Cell> GetNeighborCells(Cell cell)
    {
        List<Cell> neighborcells = new List<Cell>();
        Vector2 cellpos = cell.GetCoord();
        int x = (int)cellpos.x;
        int y = (int)cellpos.y; ;
        if (x > 0)
        {
            neighborcells.Add(Cells[x - 1, y]);
        }
        if (x < FieldSize.x - 1)
        {
            neighborcells.Add(Cells[x + 1, y]);
        }
        if (y > 0)
        {
            neighborcells.Add(Cells[x, y - 1]);
        }
        if (y < FieldSize.y - 1)
        {
            neighborcells.Add(Cells[x, y + 1]);
        }
        if (x > 0 && y > 0)
        {
            neighborcells.Add(Cells[x - 1, y - 1]);
        }
        if (x > 0 && y < FieldSize.y - 1)
        {
            neighborcells.Add(Cells[x - 1, y + 1]);
        }
        if (x < FieldSize.x - 1 && y > 0)
        {
            neighborcells.Add(Cells[x + 1, y - 1]);
        }
        if (x < FieldSize.x - 1 && y < FieldSize.y - 1)
        {
            neighborcells.Add(Cells[x + 1, y + 1]);
        }
        return neighborcells;
    }

    void InitCells()
    {
        //Debug.Log(FieldSize.ToString());
        Vector2 CurrPos = Vector2.zero;
        CurrPos.x += CellSize / 2;
        CurrPos.y += CellSize / 2;
        Cells = new Cell[(int)FieldSize.x, (int)FieldSize.y];
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell cell = GameObject.Instantiate(CellPrefab);
                cell.transform.SetParent(CellsParent, false);
                cell.transform.localPosition = CurrPos;
                cell.gameObject.name = "Cell[" + x.ToString() + "," + y.ToString() + "]";
                CurrPos.y += CellSize;
                Cells[x, y] = cell;
                cell.SetCoord(x, y);
                cell.SetState(cellstate.closed);
            }
            CurrPos.x += CellSize;
            CurrPos.y = CellSize / 2;
        }
    }

    void DestroyCells()
    {
        if (Cells == null)
            return;

        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                GameObject.Destroy(Cells[x, y].gameObject);
            }
        }
    }
    void CheckValues()
    {
        int x = int.Parse(InputX.text);
        int y = int.Parse(InputY.text);
        int bombs = int.Parse(InputBombs.text);
        if (x > 0 && y > 0 && bombs < x * y)
        {
            DestroyCells();
            FieldSize.x = x;
            FieldSize.y = y;
            BombAmount = bombs;
            CorrectValues = true;
        }
        else
        {
            CorrectValues = false;
            CheckValuesText.text = "Incorrect Data!";
            Gameended = true;
        }
    }

    public void NewGame()
    {
        CheckValues();
        if (CorrectValues)
        {
            /* if (InputX)
             {
                 int x = int.Parse(InputX.text);
                 if (x > 0)
                     FieldSize.x = x;
             }
             if (InputY)
             {
                 int y = int.Parse(InputY.text);
                 if (y > 0)
                     FieldSize.y = y;
             }
             if (InputBombs)
             {
                 int bombs = int.Parse(InputBombs.text);
                 if (bombs > 0)
                     BombAmount = bombs;
             } */
            InitCells();
            // CreateBombs(BombAmount);
            OpenedCellCount = 0;
            OpenedCells = new List<Cell>();
            OpenedFC = 0;
            Gameended = false;
            YouWin.text = "";
            YouLose.text = "";

        }
    }

    void CreateBombs(int bombstocreate)
        {
            int bombcreated = 0;
            while (bombcreated < bombstocreate)
            {
                int x = Random.Range(0, (int)FieldSize.x);
                int y = Random.Range(0, (int)FieldSize.y);
                Cell randomCell = Cells[x, y];
                if (!randomCell.IsBomb && randomCell.GetState() == cellstate.closed)
                {
                    randomCell.IsBomb = true;
                    bombcreated++;
                }
            }
        }
}
