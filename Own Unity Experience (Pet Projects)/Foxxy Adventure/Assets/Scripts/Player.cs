using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{

    // Start is called before the first frame update
    void Start()
    {
        Moving = this.transform.position;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        platformAbove = controller.GetPlatformAbove(this);
        platformUnder = controller.GetPlatformUnder(this);
        leftX = controller.GetPlatformLeft(this);
        rightX = controller.GetPlatformRight(this);
        controller.PlatformCollision(this);

        animator.SetBool("isJump", isJump);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeftStart();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRightStart();
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            MoveStop();
        if (Input.GetKeyDown(KeyCode.UpArrow))
            JumpStart();
        else if (Input.GetKeyUp(KeyCode.UpArrow))
            JumpStop();

        if (moveDirection == Vector2.left)
            MoveLeft();
        else if (moveDirection == Vector2.right)
            MoveRight();
        if (jumpDirection == Vector2.up)
            JumpUp();
        else if (jumpDirection == Vector2.down)
            JumpDown();
    }

     void OnCollisionEnter2D(Collision2D col)
     {
        controller.OnPlayerCollision(this, col);
     }
}