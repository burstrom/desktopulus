﻿using UnityEngine;
using System.Collections;

public class CreateScreen : MonoBehaviour {
	public GameObject Screen;
	public Transform Target;
	private float[] screen;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("CreateScreen")){
			
			float radius = 8.0f;
			float multiplier = 0.5f;
			
			//Initial location
			float z = radius * Mathf.Sin(multiplier*Mathf.PI);
			float x = radius * Mathf.Cos(multiplier*Mathf.PI);
			float height = 5.0f; //Does not need to be changed.

			Vector3 startPos = new Vector3(x, height, z);
			
			//Detects collisions between game objects, returns an array with all objects colliding at a position.
			var collision = Physics.OverlapSphere(startPos, 5.5f);
			
			bool looped = false;
			float sideDirection = 1.0f;
			float vertDirection = 1.0f;
			//Recalculate game position, something goes wrong here.
			while (collision.Length > 0){
				if(collision[0] == Target){
					Debug.Log(multiplier);
					break;
				}
				multiplier += 0.05f*sideDirection;
				Debug.Log(multiplier);
				//catch the base case when you reach 1 Pi, loop around to other side.
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
			
			
			GameObject newscreen = Instantiate(Screen, startPos, transform.rotation) as GameObject;
			newscreen.GetComponent<FaceThis>().target = Target;
		}
	}
}
