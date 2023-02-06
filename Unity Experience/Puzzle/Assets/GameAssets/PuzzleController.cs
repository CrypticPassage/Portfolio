using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleController : MonoBehaviour 
{
	protected List<PuzzlePart> PuzzleItems = new List<PuzzlePart>();
	protected bool bWin = false;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void AddPuzzleItem(PuzzlePart item)
	{
		if (!PuzzleItems.Contains(item))
			PuzzleItems.Add(item);
	}

	public void OnPuzzleItemPlaced()
	{
		CheckWin();
	}

	protected void CheckWin()
	{
		bWin = true;
		foreach (PuzzlePart item in PuzzleItems)
		{
			if (!item.IsPlaced())
			{
				bWin = false;
				break;
			}
		}
	}

	void OnGUI()
	{
		if (bWin)
		{
			GUI.Label(new Rect(25,50,100,30), "You win!!!");
		}

		if (GUI.Button (new Rect(10,10,100,30), "Menu"))
		{
			Application.LoadLevel("Main_Menu");
		}
	}
}
