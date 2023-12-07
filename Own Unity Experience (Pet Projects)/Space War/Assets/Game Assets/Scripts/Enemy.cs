using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Controller game_controller = null;
	public GameObject ExplosionPrefab = null;
	public GameObject bullet;
	public string SceneName = "Level";

	void Start() 
	{
		if (game_controller != null)
		{
			game_controller.AddEnemy();
		}
	}


	void OnDestroy ()
	{
		if (game_controller != null)
		{
			game_controller.RemoveEnemy();
        }
	}

	public void Shot()
	{
		GameObject shot = GameObject.Instantiate(bullet);
		if (shot)
		{
			shot.transform.position = this.transform.position + new Vector3(0, -0.7f, 0);
			game_controller.AddEnemyShot();
		}
	}
	
	void Update ()
	{


	}

	void OnGUI()
	{
		if (GameObject.FindGameObjectWithTag("Player") == null)
		{
				GUI.Label (new Rect(120,10,100,30),"Game Over");
				
				if (GUI.Button (new Rect (600, 10, 100, 30), "Restart")) 
				{
					Application.LoadLevel (SceneName);
				}
				if (GUI.Button (new Rect (600, 50, 100, 30), "Main Menu")) 
				{
					Application.LoadLevel ("Main Menu");
				}

			}
		}
			
	void OnCollisionEnter2D(Collision2D collisionInfo) 
	{
		if (collisionInfo.gameObject.tag == "Shot")
		{
			Destroy(this.gameObject);
			GameObject explosion = Instantiate(ExplosionPrefab);
			if (explosion != null)
			{
				explosion.transform.position = this.transform.position;
			}
		}
	}
}
