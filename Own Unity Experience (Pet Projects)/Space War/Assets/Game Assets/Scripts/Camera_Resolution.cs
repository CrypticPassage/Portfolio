using UnityEngine;
using System.Collections;

public class Camera_Resolution : MonoBehaviour
{
	void Start () 
	{
		Screen.SetResolution (720, 600, true);
	}
}
