using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour 
{
	public float speed;
	public Vector3 direction;
	public GameObject Bullet;
	public GameObject Player;
	public GameObject ExplosionPrefab = null;
	public GameObject ExplosionPrefab2 = null;
	private bool Shot = true;
	public int Boss_Health = 5;

	void Start () 
	{
	
	}

	void shot()
	{
		GameObject shot = GameObject.Instantiate(Bullet);
		if (shot)
		{
			shot.transform.position = this.transform.position + new Vector3(0, 0.7f, 0);
		}
		Shot = false;
		StartCoroutine(AllowShot(0.5f));
	}

	void Update () 
	{
		transform.Translate(direction * speed * Time.deltaTime);
		if (transform.position.x > 4)
		{
			direction.x = -4;
			transform.Translate(direction * speed * Time.deltaTime);
		}
		if (transform.position.x < -4)
		{
			direction.x = 4;
			transform.Translate(direction * speed * Time.deltaTime);
		}

		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			if (Shot)
			{
				shot();
			}
		}
		}

		private IEnumerator AllowShot (float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			Shot = true;
		}


	void OnCollisionEnter2D(Collision2D collisionInfo) 
 	{
		if (collisionInfo.gameObject.tag == "Shot")
		{
			Boss_Health--;
			GameObject explosion2 = Instantiate (ExplosionPrefab2);
			if (explosion2 != null) {
				explosion2.transform.position = this.transform.position + new Vector3(0,-1.2f,0);
			}

			this.gameObject.transform.rotation =  Player.transform.rotation;
			Destroy (GameObject.FindGameObjectWithTag("Shot"));
			if (Boss_Health < 1) {
				Destroy (this.gameObject);
				GameObject explosion = Instantiate (ExplosionPrefab);
				if (explosion != null) {
					explosion.transform.position = this.transform.position;
				}

			}
		}
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "Boss Health: " + Boss_Health);
	}
}
