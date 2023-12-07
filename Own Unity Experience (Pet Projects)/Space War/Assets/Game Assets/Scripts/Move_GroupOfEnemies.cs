using UnityEngine;
using System.Collections;

public class Move_GroupOfEnemies : MonoBehaviour 
{
 	public float speed=1;
 	public Vector3 direction;
	public float Limination;

 void Start () 
 {

 }
	
 void Update () 
 {
  	transform.Translate(direction * speed * Time.deltaTime);
  	if (transform.position.x > Limination)
  	{
			direction.x = -Limination;
   		transform.Translate(direction * speed * Time.deltaTime);
  	}
		if (transform.position.x < -Limination)
  	{
			direction.x = Limination;
   		transform.Translate(direction * speed * Time.deltaTime);
  	}
 }
}
