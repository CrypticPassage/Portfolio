using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    int[,,,] pieces;
    public Vector2 SpawnCoord = new Vector2(2, 23);
    private int SelectedRotation = -1;
    private int SelectedFigure = -1;
    private Vector2 CurrPosition;
    float fallDuration = 1.0f;
    float elapsedTime = 0;
    public Cell CellPrefab;
    public Cell[,] Cells = null;
    public float CellSize = 1;
    public Vector2 FieldSize = new Vector2(10, 24);
    public Transform CellsParent;
    private bool GameStarted = false;
    private bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        InitCells();
        InitPieces();
        GameStarted = true;
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStarted && !GameOver)
        {
            if (SelectedFigure < 0)
            {
                CreateFigure();
            }
            else
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= fallDuration)
                {
                    MovingDown();
                    elapsedTime = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Rotate();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                MovingDown();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                MovingLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                MovingRight();
        }
    }

    void InitPieces()
    {
        pieces = new int[7, 4, 5, 5]
        {
        { // N
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 1, 0},
        {0, 0, 2, 1, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 1, 1, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 1, 2, 0, 0},
        {0, 1, 0, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 1, 2, 0, 0},
        { 0, 0, 1, 1, 0},
        { 0, 0, 0, 0, 0}
        }
        },
        { // Square
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 2, 1, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 0, 1, 1, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 2, 1, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 0, 1, 1, 0},
        { 0, 0, 0, 0, 0}
        }
        },
        { // I
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 1, 2, 1, 1},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 1, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 2, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {1, 1, 2, 1, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 2, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 1, 0, 0}
        }
        },
        { // L
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 2, 1, 1},
        {0, 0, 1, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 1, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {1, 1, 2, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 1, 2, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 1, 0, 0}
        }
        },
        { // T
        {
        {0, 0, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 1, 2, 1, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 1, 2, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 1, 2, 1, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 0, 0, 0}
        }
        },
        { // N Reversed
        {
        {0, 0, 0, 0, 0},
        {0, 1, 0, 0, 0},
        {0, 1, 2, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 1, 1, 0, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 2, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 1, 1, 0},
        { 0, 1, 2, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0}
        }
        },
        { // L Reversed
        {
        {0, 0, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 2, 1, 1},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 1, 0, 0},
        { 0, 0, 1, 0, 0},
        { 0, 1, 2, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0}
        },
        {
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {1, 1, 2, 0, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 0, 0, 0}
        },
        {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 2, 1, 0},
        { 0, 0, 1, 0, 0},
        { 0, 0, 1, 0, 0}
        }
        }
        };
    }

    void MovingDown()
    {
        Vector2 NewPosition = CurrPosition;
        NewPosition.y--;
        if (CanMove(NewPosition, SelectedRotation))
        {
            RemovefromCell(CurrPosition, SelectedRotation);
            PlacetoCell(NewPosition, SelectedRotation);
            CurrPosition = NewPosition;
        }
        else
        {
            EndMovingFigure();
            CheckLines();
            CheckGameOver(19);
        }
    }

    void MovingLeft()
    {
        Vector2 NewPosition = CurrPosition;
        NewPosition.x--;
        if (CanMove(NewPosition, SelectedRotation))
        {
            RemovefromCell(CurrPosition, SelectedRotation);
            PlacetoCell(NewPosition, SelectedRotation);
            CurrPosition = NewPosition;
        }
    }

    void MovingRight()
    {
        Vector2 NewPosition = CurrPosition;
        NewPosition.x++;
        if (CanMove(NewPosition, SelectedRotation))
        {
            RemovefromCell(CurrPosition, SelectedRotation);
            PlacetoCell(NewPosition, SelectedRotation);
            CurrPosition = NewPosition;
        }
    }

    void Rotate()
    {
        int newRotation = SelectedRotation + 1;
        if (newRotation > 3)
            newRotation = 0;

        if (CanMove(CurrPosition, newRotation))
        {
            RemovefromCell(CurrPosition, SelectedRotation);
            PlacetoCell(CurrPosition, newRotation);
            SelectedRotation = newRotation;
        }
    }

    void CheckLines()
    {
        for (int y = 0; y < (int)FieldSize.y; y++)
        {
            int counter = 0;
            for (int x = 0; x < (int)FieldSize.x; x++)
            {
                if (Cells[x, y].IsBlock < 0)
                    counter++;
            }
            if (counter == 10)
            {
                for (int x = 0; x < (int)FieldSize.x; x++)
                {
                    Cells[x, y].IsBlock = 0;
                }
                ChangeLinesPosition(y);
                CheckLines();
            }
        }
    }

    void ChangeLinesPosition(int posy)
    {
        for (int x = 0; x < (int)FieldSize.x; x++)
        {
            for (int y = posy; y < (int)FieldSize.y; y++)
            {
                if (Cells[x, y].IsBlock == -1)
                {
                    Cells[x, y - 1].IsBlock = -1;
                    Cells[x, y].IsBlock = 0;
                }
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

    void CreateFigure()
    {
        CurrPosition = SpawnCoord;
        SelectedFigure = Random.Range(0, 7);
        SelectedRotation = 0;
        PlacetoCell(CurrPosition, SelectedRotation);
    }

    void PlacetoCell(Vector2 Coord, int rotation)
    {
        Vector2 CurrCoord = Coord;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (pieces[SelectedFigure, rotation, y, x] > 0)
                {
                    if (SelectedFigure == 0)
                    Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 2;
                    if (SelectedFigure == 1)
                        Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 3;
                    if (SelectedFigure == 2)
                        Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 4;
                    if (SelectedFigure == 3)
                        Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 5;
                    if (SelectedFigure == 4)
                        Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 6;
                    if (SelectedFigure == 5)
                        Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 7;
                    if (SelectedFigure == 6)
                        Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 8;

                }
                CurrCoord.y--;
            }
            CurrCoord.y = Coord.y;
            CurrCoord.x++;
        }
    }

    void RemovefromCell(Vector2 Coord, int rotation)
    {
        Vector2 CurrCoord = Coord;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (pieces[SelectedFigure, rotation, y, x] > 0)
                {
                    Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock = 0;
                }
                CurrCoord.y--;
            }
            CurrCoord.y = Coord.y;
            CurrCoord.x++;
        }
    }

    bool CanMove(Vector2 Coord, int rotation)
    {
        bool canMove = true;
        Vector2 CurrCoord = Coord;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (pieces[SelectedFigure, rotation, y, x] > 0)
                {
                    if (CurrCoord.y < 0 || CurrCoord.x < 0 || CurrCoord.x >= (int)FieldSize.x || Cells[(int)CurrCoord.x, (int)CurrCoord.y].IsBlock == -1)
                    {
                        canMove = false;
                        break;
                    }
                }
                CurrCoord.y--;
            }
            if (!canMove)
            {
                break;
            }
            CurrCoord.y = Coord.y;
            CurrCoord.x++;
        }
        return canMove;
    }

    void EndMovingFigure()
    {
        for (int x = 0; x < FieldSize.x; x++)
        {
            for (int y = 0; y < FieldSize.y; y++)
            {
                Cell currCell = Cells[x, y];
                if (currCell.IsBlock > 1)
                    currCell.IsBlock = -1;
            }
        }
        SelectedFigure = -1;
    }

    void CheckGameOver(int posy)
    {
        for (int x = 0; x < FieldSize.x; x++)
        {
            if (Cells[x, posy].IsBlock > 0)
            {
                GameOver = true;
                break;
            }
        }
    }
}
