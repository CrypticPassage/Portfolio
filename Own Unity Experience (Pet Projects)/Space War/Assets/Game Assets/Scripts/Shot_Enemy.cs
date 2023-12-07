using UnityEngine;
using System.Collections;

public class Shot_Enemy : MonoBehaviour
{
	public float limination;
	public Vector3 speed;
	public Controller game_controller = null;
	
	void Start() 
	{
		game_controller = GameObject.FindObjectOfType<Controller>();
	}
	
	void OnDestroy ()
	{
		if (game_controller != null)
		{
			game_controller.RemoveEnemyShot();
		}
	}
	
	void Update () 
	{
		transform.Translate(speed * Time.deltaTime);
		if (transform.position.y < limination)
		{
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collisionInfo) 
	{	 
		if (collisionInfo.gameObject.tag == "Player")
		{
			Destroy(this.gameObject);		
		}
	}
}
