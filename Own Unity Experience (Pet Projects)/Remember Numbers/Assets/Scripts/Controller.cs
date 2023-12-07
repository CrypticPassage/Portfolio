using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public List<GameObject> Cells = new List<GameObject>();
    int[] randomvalarr = new int[10];
    enum state
    {
        none = 1,
        comp = 2,
        player = 3
    }

    state gamestate = state.none;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        BeginCompState();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate == state.comp)
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                HideNumbers();
                gamestate = state.player;
            }
        }
        else if (gamestate == state.player)
        {
            if (Input.GetMouseButtonUp(0))
                MouseClick();
        }
    }
    void BeginCompState()
    {
        randomvalarr = new int[10];
        timer = 0;
        HideNumbers();
        gamestate = state.comp;
        Randomize();
    }

    void RemoveArrEl(ref int[] array, int index)
    {
        int[] newArray = new int[array.Length - 1];
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

    void MouseClick()
    {
        //Debug.Log("Click on Controller");
        for (int i = 0; i < Cells.Count; i++)
        {
            GameObject currentCell = Cells[i];
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
                    Debug.Log(GetNumber(currentCell));
                    if (GetNumber(currentCell) == randomvalarr[0])
                    {
                        Debug.Log("Correct!");
                        Cell CurrComp = currentCell.GetComponent<Cell>();
                        CurrComp.Border.SetActive(true);
                        Text text = currentCell.GetComponentInChildren<Text>();
                        int currentCellNumber = GetNumber(currentCell);
                        text.text = currentCellNumber.ToString();
                        RemoveArrEl(ref randomvalarr, 0);
                    }
                    else
                    {
                        Debug.Log("You Lose!");
                        BeginCompState();
                    }
                }
            }
        }
    }

    void HideNumbers()
    {
        for (int i = 0; i < Cells.Count; i++)
        {
            GameObject currCell = Cells[i];
            Text text = currCell.GetComponentInChildren<Text>();
            Cell CurrComp = currCell.GetComponent<Cell>();
            CurrComp.Border.SetActive(false);
            if (text != null)
            {
                text.text = "";
            }

        }
    }

    void Swap(ref int e1, ref int e2)
    {
        var temp = e1;
        e1 = e2;
        e2 = temp;
    }

    int[] BubbleSort(int[] array)
    {
        var len = array.Length;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Swap(ref array[j], ref array[j + 1]);
                }
            }
        }

        return array;
    }

    void Randomize()
    {
        int val = Random.Range(0, 100);
        // GameObject[] randomcells = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            randomvalarr[i] = val;
            int randomindex = Random.Range(0, 100);
            GameObject randomcell = Cells[randomindex];
            Text text = randomcell.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = val.ToString();
                Cell cell = randomcell.GetComponent<Cell>();
                if (cell)
                {
                    cell.RememberNumber(val);
                }
                val = Random.Range(0, 100);
            }
        }

        BubbleSort(randomvalarr);
        
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(randomvalarr[i]);
        }
    }



    int GetNumber(GameObject cell)
    {
        Cell cell1 = cell.GetComponentInChildren<Cell>();
        if (cell1)
        {
            return cell1.GetRememberedNumber();
        }
        return -1;
    }

}
