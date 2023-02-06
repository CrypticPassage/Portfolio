using UnityEngine;
using System.Collections;

public class The_End : MonoBehaviour 
{
	void OnGUI()
	{
		GUI.Label (new Rect(200,200,300,300),"Congratulations, you have killed all the alien invaders and save the planet from enslavement.Thanks for playing!");

		if	(GUI.Button (new Rect(550,420,100,30),"Main Menu"))
		{
			Application.LoadLevel("Main Menu");
		}
	}
}
