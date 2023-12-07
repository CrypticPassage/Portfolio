using UnityEngine;
using System.Collections;

public class Shot_Player : MonoBehaviour
{
	public float limination;
	public Vector3 speed;
			
 	void Start () 
 	{

	}

 	void Update () 
 	{
  		transform.Translate(speed * Time.deltaTime);
  		if (transform.position.y > limination)
  		{
   			Destroy(this.gameObject);
  		}
 	}

	void OnCollisionEnter2D(Collision2D collisionInfo) 
	{	 
		if (collisionInfo.gameObject.tag == "Enemy")
		{
			Destroy(this.gameObject);

	    }
	}
}
