using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
	public float ExplosionLifeTime = 0.5f;
	
	void Start() 
	{
		Destroy(this.gameObject, ExplosionLifeTime);
	}
}
