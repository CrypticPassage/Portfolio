using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour 
{
	public string startSceneName = "Level2";
	void OnGUI()
	{
		if (GUI.Button (new Rect(10,10,100,30),"Start"))
	    {
			print ("Start Game");
			Application.LoadLevel(startSceneName);
		}
		if (GUI.Button (new Rect(10,70,100,30),"Exit"))
		{
			Application.Quit();
		}
	}
}

