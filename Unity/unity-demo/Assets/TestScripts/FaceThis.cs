using UnityEngine;
using System.Collections;

public class FaceThis : MonoBehaviour {

	public Transform target;

	void Update () {
		//Every frame, update the rotation of the screen objects so they look at the actor.
		Vector3 tar = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(-tar);

	}
	
}
