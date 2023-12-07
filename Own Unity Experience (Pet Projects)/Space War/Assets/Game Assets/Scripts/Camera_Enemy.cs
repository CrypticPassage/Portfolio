using UnityEngine;
using System.Collections;

public class Camera_Enemy : MonoBehaviour
{
	void OnGUI()
	{
		if (GameObject.FindGameObjectWithTag("Player") == null)
		{
			GUI.Label (new Rect(120,10,100,30),"Game Over");
			
			if (GUI.Button (new Rect (600, 10, 100, 30), "Restart")) 
			{
				Application.LoadLevel ("Level5");
			}
			if (GUI.Button (new Rect (600, 50, 100, 30), "Main Menu")) 
			{
				Application.LoadLevel ("Main Menu");
			}
			
		}

		if (GameObject.FindGameObjectWithTag ("Boss") == null) 
		{	
			Application.LoadLevel("The End");
		}
	}
}
