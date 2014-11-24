using UnityEngine;
using System.Collections;

//Basic camera controls using keyboard. This is to be replaced by the oculus.

public class TestController : MonoBehaviour {
	//A little bit of drag to stop the camera from constantly accellerating.
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
