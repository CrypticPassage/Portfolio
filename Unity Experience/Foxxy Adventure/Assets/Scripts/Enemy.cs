using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    public float minX = 0;
    public float maxX = 0;

    enum direction
    {
        none = 0,
        left = 1,
        right = 2
    }

    direction moveDir = direction.none;

    // Start is called before the first frame update
    void Start()
    {
        mycollider = this.GetComponent<BoxCollider2D>();
        Moving = this.transform.position;

        isAlive = true;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if (moveDir == direction.none)
            moveDir = (direction)Random.Range(1, 3);

        if (moveDir == direction.right)
            MoveRight();
        else if (moveDir == direction.left)
            MoveLeft();

        if (Moving.x - (mycollider.size.x / 2) + 0.1f < minX)
        {
            moveDir = direction.right;
            MoveRightStart();
        }
        if (Moving.x + (mycollider.size.x / 2) - 0.1f > maxX)
        {
            moveDir = direction.left;
            MoveLeftStart();
        }
    }
}