using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int IsBlock = 0;
    public SpriteRenderer CellColor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsBlock == 0)
            CellColor.color = Color.black;
        else if (IsBlock == 2)
            CellColor.color = Color.red;
        else if (IsBlock == 3)
            CellColor.color = Color.blue;
        else if (IsBlock == 4)
            CellColor.color = Color.yellow;
        else if (IsBlock == 5)
            CellColor.color = Color.gray;
        else if (IsBlock == 6)
            CellColor.color = Color.green;
        else if (IsBlock == 7)
            CellColor.color = Color.cyan;
        else if (IsBlock == 8)
            CellColor.color = Color.magenta;
    }
}
