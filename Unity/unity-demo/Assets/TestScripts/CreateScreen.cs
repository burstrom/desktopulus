using UnityEngine;
using System.Collections;

public class CreateScreen : MonoBehaviour {
	public GameObject Screen;
	public Transform Target;
	private float[] screen;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("CreateScreen")){
			//Radius, this is the distance between the actor and the screens.
			float radius = 8.0f;
			//This is the multiplier for placing screens in a multiplier*pi half circle around the actor. Starts at 0.5 to place directly in front.
			float multiplier = 0.5f;
			
			//Initial location
			float z = radius * Mathf.Sin(multiplier*Mathf.PI);
			float x = radius * Mathf.Cos(multiplier*Mathf.PI);
			float height = 5.0f; //Does not need to be changed unless you also change the initial position of the actor.
			Vector3 startPos = new Vector3(x, height, z);
			
			//Detects collisions between game objects, returns an array with all objects colliding at a position. The float is the
			//sensitivity of the sphere, basically it needs to be slightly larger than the screens width.
			var collision = Physics.OverlapSphere(startPos, 5.5f);
			
			//Prevent infinite loops if no position can be found to add a screen.
			bool looped = false;
			
			//What direction are we adding new objects, swaps between -1 and 1.
			float sideDirection = 1.0f;
			float vertDirection = 1.0f;
			
			//Try new positions to add the screen while a collision is detected.
			while (collision.Length > 0){
				if(collision[0] == Target){
					Debug.Log(multiplier);
					break;
				}
				multiplier += 0.05f*sideDirection;
				Debug.Log(multiplier);
				//catch the base case when you reach 1 Pi, prevents screens from being spawned behind the actor.
				//If it can spawn no more at a height-level it alternatingly jumps above and below the actor to spawn.
				if (multiplier > 1.0f || multiplier < 0.0f){
					if(looped){
						startPos.y += 8.0f*vertDirection;
						vertDirection *= -2.0f;
						Debug.Log ("I looped all around");
						looped = false;
					} else {
						sideDirection *= -1.0f;
						multiplier = 0.5f;
						looped = true;
					}
				}
				startPos.z = radius * Mathf.Sin(multiplier*Mathf.PI);
				startPos.x = radius * Mathf.Cos(multiplier*Mathf.PI);
				collision = Physics.OverlapSphere(startPos, 5.5f);
			}
			
			//Actually create the screen.
			GameObject newscreen = Instantiate(Screen, startPos, transform.rotation) as GameObject;
			newscreen.GetComponent<FaceThis>().target = Target;
		}
	}
}
