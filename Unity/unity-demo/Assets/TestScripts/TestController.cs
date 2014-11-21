using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {
	void Start () {
		rigidbody.drag = 2;
	}
	void FixedUpdate(){
		
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal*2.0f, 0.0f, moveVertical*2.0f);
		rigidbody.AddForce (movement);
		
	}
	
}
