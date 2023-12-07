using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour 
{
	public float LoadTime = 1f;

	void Start()
	{
		Screen.SetResolution (720, 600, true);
	}

	void OnGUI()
	{
		if	(GUI.Button (new Rect(10,10,100,30),"New Game"))
		{
			Application.LoadLevel("Level");
		}
		if	(GUI.Button (new Rect(10,535,100,30),"Exit"))
		{
			Application.Quit();
		}

		GUI.Label (new Rect(540,10,350,100),"Options:");
		GUI.Label (new Rect(540,25,350,100),"Left Arrow - Move left;");
		GUI.Label (new Rect(540,40,350,100),"Right Arrow - Move Right;");
		GUI.Label (new Rect(540,55,350,100),"Right Control or Space - Fire.");
		GUI.Label (new Rect(540,70,350,100),"Escape - Exit to Main Menu.");
    	GUI.Label (new Rect(420,535,300,300),"C2016,Nikolayenko Dmytro.");
		GUI.Label (new Rect(420,550,300,300),"Special Thanks to Nikolayenko Sergey.");
	}	
}
