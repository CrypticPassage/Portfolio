using UnityEngine;
using System.Collections;

public class Move_Player : MonoBehaviour 
{
	public float speed = 0;
 	public GameObject bullet;
 	public GameObject player;	
 	public float x = 4;
 	public float y = -9;
 	private bool CanShot = true;
	public GameObject ExplosionPrefab = null;

 	void Start () 
 	{
  		player = (GameObject)this.gameObject;
 	}

 	void Update ()
 	{
  		if (Input.GetKey(KeyCode.LeftArrow)) 
  		{
			transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
  		}
 		if (Input.GetKey(KeyCode.RightArrow)) 
  		{
   			transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
  		}
  		if (transform.position.x>x)
  		{
   			transform.position = new Vector3(x, -2, 0);
  		}
  		if (transform.position.x<y)
  		{
   			transform.position = new Vector3(y, -2, 0);
  		}	
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl))
  		{
  			if (CanShot)
  			{
				Shot();
			}
  		}
	 }

	void Shot()
	{
		GameObject shot = GameObject.Instantiate(bullet);
		if (shot)
		{
			shot.transform.position = this.transform.position + new Vector3(0, 0.7f, 0);
		}
		CanShot = false;
		StartCoroutine(AllowShot(0.5f));
	}
 
	private IEnumerator AllowShot (float waitTime)
 	{
 		yield return new WaitForSeconds(waitTime);
  		CanShot = true;
 	}

	void OnCollisionEnter2D(Collision2D collisionInfo) 
	{	 
		if (collisionInfo.gameObject.tag == "ShotEnemy")
		{
			Destroy(this.gameObject);
			GameObject explosion = Instantiate(ExplosionPrefab);
			if (explosion != null)
			{
				explosion.transform.position = this.transform.position;
			}
		}
	}
}
