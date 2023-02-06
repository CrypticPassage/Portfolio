using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Cell CellPrefab;
    private Cell[,] Cells = null;
    private Cell[] SortedCells = new Cell[16];
    public float CellSize = 1;
    public Transform CellsParent;
    public Cell SwapParent;
    public Vector2 FieldSize = new Vector2(4, 4);
    private Cell ClickedPreviousCell;
    private Cell ZeroCell;
    private CellPlate AnimPlate1;
    private CellPlate AnimPlate2;
    private int Win = 0;
    private bool anim = false;
    private float duration = 0.3f;
    private float elapsedtime;

    // Start is called before the first frame update
    void Start()
    {
        ClickedPreviousCell = null;
        ZeroCell = null;
        InitCells();
        Randomize();
        SortCells();
        HideZeroCell();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !anim)
            MouseClick();

        if (anim)
        {
            elapsedtime += Time.deltaTime;
            float completetime = elapsedtime /duration;

            AnimPlate1.transform.position = Vector3.Lerp(AnimPlate1.AnimStartPos, AnimPlate1.AnimEndPos, completetime);
            AnimPlate2.transform.position = Vector3.Lerp(AnimPlate2.AnimStartPos, AnimPlate2.AnimEndPos, completetime);
            if (completetime >= 1)
            {
                anim = false;
            }
        }
    }

    void MouseClick()
    {
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell currentCell = Cells[x, y];
                if (currentCell == null)
                    continue;
                BoxCollider2D col = currentCell.GetComponentInChildren<BoxCollider2D>();
                if (col != null)
                {
                    Vector2 min = col.bounds.min;
                    Vector2 max = col.bounds.max;
                    Vector2 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (click.x > min.x && click.x < max.x && click.y > min.y && click.y < max.y)
                    {
                        if (!currentCell.IsBorders && !currentCell.IsCellPushed && ClickedPreviousCell == null)
                        {
                            currentCell.Borders.SetActive(true);
                            currentCell.IsBorders = true;
                            currentCell.IsCellPushed = true;
                            ClickedPreviousCell = currentCell;
                        }
                    else if (currentCell.IsBorders && currentCell.IsCellPushed && ClickedPreviousCell != null)
                        {
                            DeactivateCell(currentCell);
                        }
                    else
                        {
                            Swap(currentCell);
                        }
                    }
                }
            }
        }
    }

    void DeactivateCell(Cell currCell)
    {
        currCell.Borders.SetActive(false);
        currCell.IsBorders = false;
        currCell.IsCellPushed = false;
        ClickedPreviousCell = null;
    }

    void Swap(Cell currCell)
    {
        if (currCell.Plate.Number.text == "" || ClickedPreviousCell.Plate.Number.text == "")
        {
            if (currCell.transform.position.x == ClickedPreviousCell.transform.position.x + 1 && currCell.transform.position.y == ClickedPreviousCell.transform.position.y ||
            currCell.transform.position.x == ClickedPreviousCell.transform.position.x - 1 && currCell.transform.position.y == ClickedPreviousCell.transform.position.y ||
            currCell.transform.position.x == ClickedPreviousCell.transform.position.x && currCell.transform.position.y == ClickedPreviousCell.transform.position.y + 1 ||
            currCell.transform.position.x == ClickedPreviousCell.transform.position.x && currCell.transform.position.y == ClickedPreviousCell.transform.position.y - 1)
            {
                CellPlate tempPlate = ClickedPreviousCell.Plate;

                ClickedPreviousCell.Plate = currCell.Plate;
                ClickedPreviousCell.Plate.transform.SetParent(ClickedPreviousCell.transform, true);

                currCell.Plate = tempPlate;
                currCell.Plate.transform.SetParent(currCell.transform, true);

                anim = true;
                AnimPlate1 = currCell.Plate;
                AnimPlate2 = ClickedPreviousCell.Plate;
                AnimPlate1.AnimStartPos = AnimPlate1.transform.position;
                AnimPlate1.AnimEndPos = AnimPlate2.transform.position;
                AnimPlate2.AnimStartPos = AnimPlate2.transform.position;
                AnimPlate2.AnimEndPos = AnimPlate1.transform.position;
                elapsedtime = 0;
                anim = false;

                if (ClickedPreviousCell.Plate.Number.text == "")
                    ZeroCell = ClickedPreviousCell;
                if (currCell.Plate.Number.text == "")
                    ZeroCell = currCell;
                Debug.Log(ZeroCell);
                CheckWin(ZeroCell);
            }
        }
        DeactivateCell(ClickedPreviousCell);
    }

    void CheckWin(Cell WinCell)
    {
        Cell WCell = Cells[3, 0];
        if (WinCell.transform.position == WCell.transform.position) 
        {
            Debug.Log("Zero is on a point! Begin check for Win!");
            Cell[] currentCellsPosition = new Cell[16];
            int c = 0;
            for (int y = (int)FieldSize.y - 1; y >= 0; y--)
            {
                for (int x = 0; x < FieldSize.x; x++)
                {
                    currentCellsPosition[c] = Cells[x, y];
                    c++;
                }
            }
            RemoveArrEl(ref currentCellsPosition, 15);
            for (int i = 0; i < currentCellsPosition.Length; i++)
                Debug.Log(currentCellsPosition[i].Plate.Number.text);
            for (int j = 0; j < currentCellsPosition.Length; j++)
            {
                if (currentCellsPosition[j].Plate.Number.text == SortedCells[j].Plate.Number.text)
                    Win++;
                if (Win == 15)
                    Debug.Log("You Win!");
            }
        }
    }

    void InitCells()
    {
        Vector2 CurrPos = Vector2.zero;
        CurrPos.x += CellSize / 2;
        CurrPos.y += CellSize / 2;
        Cells = new Cell[(int)FieldSize.x, (int)FieldSize.y];
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell cell = GameObject.Instantiate(CellPrefab);
                cell.Borders.SetActive(false);
                cell.transform.SetParent(CellsParent, false);
                cell.transform.localPosition = CurrPos;
                cell.gameObject.name = "Cell[" + x.ToString() + "," + y.ToString() + "]";
                CurrPos.y += CellSize;
                Cells[x, y] = cell;
            }
            CurrPos.x += CellSize;
            CurrPos.y = CellSize / 2;
        }
    }

    void SortCells()
    {
        int c = 0;
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                SortedCells[c] = Cells[x, y];
                c++;
            }
        }
        for (int startIndex = 0; startIndex < SortedCells.Length - 1; ++startIndex)
        {
            int smallestIndex = startIndex;
            for (int currentIndex = startIndex + 1; currentIndex < SortedCells.Length; ++currentIndex)
            {
                int count1 = int.Parse(SortedCells[currentIndex].Plate.Number.text);
                int count2 = int.Parse(SortedCells[smallestIndex].Plate.Number.text);
                if (count1 < count2)
                    smallestIndex = currentIndex;
            }
            Cell tmp = SortedCells[startIndex];
            SortedCells[startIndex] = SortedCells[smallestIndex];
            SortedCells[smallestIndex] = tmp;
        }
        RemoveArrEl(ref SortedCells, 0);
        for (int i = 0; i < SortedCells.Length; i++)
            Debug.Log(SortedCells[i].Plate.Number.text);
    }

    void RemoveArrEl(ref Cell[] array, int index)
    {
        Cell[] newArray = new Cell[array.Length - 1];
        for (int i = 0; i < index; i++)
        {
            newArray[i] = array[i];
        }
        for (int i = index + 1; i < array.Length; i++)
        {
            newArray[i - 1] = array[i];
        }
        array = newArray;
    }

    void HideZeroCell()
    {
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell currentCell = Cells[x, y];
                if (currentCell.Plate.Number.text == "0")
                {
                    currentCell.Plate.Number.text = "";
                    ZeroCell = currentCell;
                }
            }
        }
    }

    void Randomize()
    {
        int val = 0;
        int[,] valarr = new int[(int)FieldSize.x, (int)FieldSize.y];
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
               Cell currentCell = Cells[x, y];
               currentCell.Plate.Number.text = val.ToString();
               valarr[x, y] = val;
               val++;
            }
        }
        for (int x = (int)FieldSize.x - 1; x >= 1; x--)
        {
            for (int y = (int)FieldSize.y - 1; y >= 1; y--)
            {
                int newx = Random.Range(0, x);
                int newy = Random.Range(0, y);
                int tmp = valarr[newx, newy];
                valarr[newx, newy] = valarr[x, y];
                valarr[x, y] = tmp;
            }
        }
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell currentCell = Cells[x, y];
                currentCell.Plate.Number.text = valarr[x, y].ToString();
            }
        }
    }
}