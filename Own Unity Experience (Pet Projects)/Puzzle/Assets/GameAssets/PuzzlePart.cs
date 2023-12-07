using UnityEngine;
using System.Collections;

public class PuzzlePart : MonoBehaviour 
{
	public Vector2 RightPosition;
	public Vector2 StartPosition;
	public Vector2 Offset; 

	protected bool bPlaced = false;
	protected float Precision = 0.2f;

	public Color ColorNotPlaced = Color.gray;
	protected Color ColorPlaced = Color.gray;

	protected PuzzleController pzController = null;
 
	// Use this for initialization
	void Start () 
	{
		gameObject.transform.position = StartPosition;
		bPlaced = false;
		ColorPlaced = gameObject.GetComponent<Renderer>().material.color;
		gameObject.GetComponent<Renderer>().material.color = ColorNotPlaced;
		pzController = Camera.main.gameObject.GetComponent<PuzzleController>();
		if (pzController != null)
			pzController.AddPuzzleItem(this);
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseDrag()
	{
		if (bPlaced == true)
			return;

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPosition.x += Offset.x;
		mouseWorldPosition.y += Offset.y;
		mouseWorldPosition.z = 0.0f;
		gameObject.transform.position = mouseWorldPosition;
		gameObject.GetComponent<Renderer>().sortingOrder=2;


	}

	void OnMouseUp()
	{
		if ((Mathf.Abs(gameObject.transform.position.x - RightPosition.x) < Precision) && (Mathf.Abs(gameObject.transform.position.y - RightPosition.y) < Precision))
		{
			gameObject.transform.position = RightPosition;
			bPlaced = true;
			gameObject.GetComponent<Renderer>().sortingOrder=1;
			gameObject.GetComponent<Renderer>().material.color = ColorPlaced;
			if (pzController != null)
				pzController.OnPuzzleItemPlaced();
		}
	}

	public bool IsPlaced()
	{
		return bPlaced;
	}
}
