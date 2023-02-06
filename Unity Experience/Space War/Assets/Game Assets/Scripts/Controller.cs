using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	private bool win = false;
	public string NextScene = "Level2";

	private int NumEnemies = 0;
	public void AddEnemy()
	{
		NumEnemies++;
	}

	public void RemoveEnemy()
	{
		NumEnemies--;
		if (NumEnemies == 0)
		{
			win = true;
		}
	}

	public int AllowNumEnemyShots = 1;
	private int NumEnemyShots = 0;

	public void AddEnemyShot()
	{
		NumEnemyShots++;
	}
	
	public void RemoveEnemyShot()
	{
		NumEnemyShots--;
	}

	public bool IsEnemyCanShot()
	{
		bool canShot = NumEnemyShots < AllowNumEnemyShots;
		return canShot;
	}

	void Start ()
	{

	}

	void Update()
	{
		if (IsEnemyCanShot())
		{
			Enemy[] shooters = GameObject.FindObjectsOfType<Enemy>();
			int NumEnemies = shooters.Length;
			int selected = Random.Range(0,NumEnemies);
			shooters[selected].Shot();
		}
		if (Input.GetKey(KeyCode.Escape)) 
		{
			Application.LoadLevel("Main Menu");
		}
	}
		
	void OnGUI()
	{
		if (win)
		{
		 	if (GUI.Button (new Rect(10,10,300,30),"You won!!!Press To Go On Next Level."))
			{
				Application.LoadLevel(NextScene);
			}
			if (GUI.Button (new Rect (600, 10, 100, 30), "Main Menu")) 
			{
				Application.LoadLevel ("Main Menu");
			}
		}
		else 
		{
			GUI.Label (new Rect(10,10,100,30), "Enemies: " + NumEnemies.ToString());
		}
	}
}
