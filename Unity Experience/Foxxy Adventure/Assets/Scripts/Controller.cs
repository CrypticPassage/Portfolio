using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Platform[] platforms;
    public int diamondsAmount = 3;
    public AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        SortPlatforms();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public Platform GetPlatformAbove(Person person)
    {
        Platform above = null;
        for (int i = 0; i < platforms.Length; i++)
        {
            if (person.Moving.x >= platforms[i].collider.bounds.min.x && person.Moving.x <= platforms[i].collider.bounds.max.x && person.Moving.y <= platforms[i].collider.bounds.min.y)
            {
                above = platforms[i];
                break;
            }
        }
        return above;
    }

    public Platform GetPlatformUnder(Person person)
    {
        Platform under = null;
        for (int i = 0; i < platforms.Length; i++)
        {
            for (int k = 1; k < platforms.Length; k++)
            {
                if (person.Moving.x >= platforms[i].collider.bounds.min.x && person.Moving.x <= platforms[i].collider.bounds.max.x && person.Moving.y >= platforms[i].collider.bounds.min.y)
                {
                    under = platforms[i];
                    break;
                }
            }
        }
        return under;
    }

    public Platform GetPlatformLeft(Person person)
    {
        Platform left = null;
        for (int i = 0; i < platforms.Length; i++)
        {
            for (int k = 1; k < platforms.Length; k++)
            {
                if (person.Moving.x >= platforms[i].collider.bounds.max.x && person.Moving.x >= platforms[k].collider.bounds.max.x)
                {
                    if (platforms[i].collider.bounds.max.x <= platforms[k].collider.bounds.max.x)
                    {
                        person.leftX = platforms[k];
                    }
                }
                if (person.Moving.x >= platforms[i].collider.bounds.max.x && person.Moving.x <= platforms[k].collider.bounds.max.x)
                {
                    person.leftX = platforms[i];
                }
            }
        }
        return left;
    }

    public Platform GetPlatformRight(Person person)
    {
        Platform right = null;
        int temp = platforms.Length - 1;

        for (int j = temp; j >= 0; j--)
        {
            for (int n = temp - 1; n >= 0; n--)
            {
                if (person.Moving.x <= platforms[j].collider.bounds.min.x && person.Moving.x <= platforms[n].collider.bounds.min.x)
                {
                    if (platforms[j].collider.bounds.min.x >= platforms[n].collider.bounds.min.x)
                    {
                        person.rightX = platforms[n];
                    }
                }
                if (person.Moving.x <= platforms[j].collider.bounds.min.x && person.Moving.x >= platforms[n].collider.bounds.min.x)
                {
                    person.rightX = platforms[j];
                }
            }
        }
        return right;
    }

    public void PlatformCollision(Person person)
    {
        if (person.platformAbove)
        {
            if (person.Moving.y + person.offSet >= person.platformAbove.collider.bounds.min.y)
                person.jumpDirection = Vector2.down;
        }
        if (person.platformUnder)
        {
            if (person.previousPlatform != person.platformUnder)
            {
                person.jumpDirection = Vector2.down;
            }

            person.jumpStartY = person.platformUnder.collider.bounds.max.y + person.offSet;
            if (!person.platformUnder.isOnPlatform && !person.isJump)
            {
                if (person.Moving.y - person.offSet > person.platformUnder.collider.bounds.max.y)
                    person.Moving.y -= person.speed * Time.deltaTime;
                else if (person.Moving.y - person.offSet <= person.platformUnder.collider.bounds.max.y && person.Moving.y + person.offSet > person.platformUnder.collider.bounds.min.y)
                {
                    person.Moving.y = person.platformUnder.collider.bounds.max.y + person.offSet;
                    person.platformUnder.isOnPlatform = true;
                }
            }
            if (person.platformUnder.isOnPlatform)
                person.jumpLimit = person.platformUnder.collider.bounds.max.y + person.offSet + 5f;

            person.previousPlatform = person.platformUnder;

            if (person.Moving.x > person.platformUnder.collider.bounds.max.x || person.Moving.x <= person.platformUnder.collider.bounds.min.x)
                person.platformUnder = null;
            if (person.leftX)
                person.leftX = null;
            if (person.rightX)
                person.rightX = null;
        }
    }        

    private void SortPlatforms()
    {
        for (int i = 1; i < platforms.Length; i++)
        {
            for (int j = 0; j < platforms.Length - i; j++)
            {
                if (platforms[j].collider.bounds.min.x > platforms[j + 1].collider.bounds.min.x)
                {
                    Platform temp = platforms[j];
                    platforms[j] = platforms[j + 1];
                    platforms[j + 1] = temp;
                }
            }
        }
    }

    public void OnPlayerCollision(Person player, Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (player.transform.position.y > col.transform.position.y + (col.collider.bounds.size.y / 2))
            {
                Person enemy = col.gameObject.GetComponent<Person>();
                enemy.Death();
            }
            else
            {
                player.Death();
            }
        }
        else if (col.gameObject.tag == "Diamond")
        {
            Destroy(col.gameObject);
            diamondsAmount--;
        }
        else if (col.gameObject.tag == "End")
        {
            if (diamondsAmount <= 0)
            {
                if (winSound)
                    winSound.Play();
            }
        }
        else if (col.gameObject.tag == "VerticalBlock")
        {
            float currDist = Mathf.Abs(player.mycollider.bounds.center.x - col.collider.bounds.center.x);
            float needDist = player.mycollider.bounds.size.x / 2 + col.collider.bounds.size.x / 2;
            float dx = needDist - currDist + 0.05f;
            Vector3 pos = player.transform.position;
            if (player.mycollider.bounds.center.x > col.collider.bounds.center.x)
                pos.x += dx;
            else
                pos.x -= dx;
            player.MoveTo(pos);
            player.MoveStop();
        }
    }
}