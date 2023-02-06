using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool IsBomb = false;
    public Text text;
    public GameObject CellClosed;
    public GameObject CellOpened;
    public GameObject Flag;
    public GameObject Bomb;
    public BoxCollider2D coll;
    private Controller.cellstate state = Controller.cellstate.closed;
    private Vector2 coord;

    public void SetCoord(int x, int y)
    {
        coord.x = x;
        coord.y = y;
    }

    public Vector2 GetCoord()
    {
        return coord;
    }

    public void SetState(Controller.cellstate newstate)
    {
        state = newstate;
        if (state == Controller.cellstate.closed)
        {
            if (Flag)
                Flag.SetActive(false);
            if (Bomb)
                Bomb.SetActive(false);
        }
        else if (state == Controller.cellstate.spotted)
        {
            if (Flag)
                Flag.SetActive(true);
        }
        else if (state == Controller.cellstate.opened)
        {
            if (CellClosed)
                CellClosed.SetActive(false);
            if (Flag)
                Flag.SetActive(false);
        }
        else if (state == Controller.cellstate.exploded)
        {
            if (CellClosed)
                CellClosed.SetActive(false);
            if (Bomb)
                Bomb.SetActive(true);
        }
    }

    public Controller.cellstate GetState()
    {
        return state;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
