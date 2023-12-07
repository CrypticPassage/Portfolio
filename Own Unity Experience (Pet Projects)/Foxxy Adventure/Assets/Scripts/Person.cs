using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public BoxCollider2D mycollider;
    public Controller controller;
    public float speed = 6;
    public Vector3 Moving;
    public Vector2 moveDirection = Vector2.zero;
    public Vector2 jumpDirection = Vector2.zero;
    public float offSet = 0;
    public float jumpStartY = 0;
    public float jumpLimit = 0;
    public Platform platformAbove = null;
    public Platform platformUnder = null;
    public Platform previousPlatform = null;
    public Platform leftX = null;
    public Platform rightX = null;
    public bool isJump = false;
    public bool isRunning = false;
    public bool isAlive = true;
    public Animator animator = null;
    public Animation deathAnim = null;
    public SpriteRenderer spriteRenderer = null;
    public bool flipOnLeft = true;
    public AudioSource jumpSound;
    public AudioSource moveSound;
    public AudioSource deathSound;

    protected void Update()
    {
        this.transform.position = Moving;

        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isAlive", isAlive);
    }

    protected virtual void MoveLeftStart()
    {
        moveDirection = Vector2.left;
        isRunning = true;
        spriteRenderer.flipX = flipOnLeft;
    }

    protected virtual void MoveRightStart()
    {
        moveDirection = Vector2.right;
        isRunning = true;
        spriteRenderer.flipX = !flipOnLeft;
    }

    public virtual void MoveStop()
    {
        moveDirection = Vector2.zero;
        isRunning = false;
        if (moveSound)
            moveSound.Stop();
    }

    protected virtual void JumpStart()
    {
        if (moveSound)
            moveSound.Stop();
        if (!isJump)
        {
            if (jumpSound)
                jumpSound.Play();
        }
        isJump = true;
        if (jumpDirection == Vector2.zero)
        {
            jumpStartY = Moving.y;
            jumpDirection = Vector2.up;
        }
    }

    protected virtual void JumpStop()
    {
        jumpDirection = Vector2.down;
    }

    protected virtual void MoveLeft()
    {
        Moving.x -= speed * Time.deltaTime;
        if (jumpDirection == Vector2.zero)
        {
            if (moveSound && !moveSound.isPlaying)
                moveSound.Play();
        }
    }

    protected virtual void MoveRight()
    {
        Moving.x += speed * Time.deltaTime;
        if (jumpDirection == Vector2.zero)
        {
            if (moveSound && !moveSound.isPlaying)
                moveSound.Play();
        }

    }

    protected virtual void JumpUp()
    {
        if (Moving.y < jumpLimit)
        {
            Moving.y += speed * Time.deltaTime;
        }
        else
        {
            jumpDirection = Vector2.down;
        }
    }

    protected virtual void JumpDown()
    {
        Moving.y -= speed * Time.deltaTime;
        if (Moving.y < jumpStartY)
        {
            jumpDirection = Vector2.zero;
            Moving.y = jumpStartY;
            isJump = false;
        }
        if (moveSound && moveSound.isPlaying)
            moveSound.Stop();
    }

    public void Death()
    {
        isAlive = false;
        isRunning = false;

        if (moveSound)
            moveSound.Stop();

        if (deathSound)
            deathSound.Play();

        Destroy(this.gameObject, 0.25f);
    }

    public void MoveTo(Vector3 toPos)
    {
        Moving = toPos;
    }
}