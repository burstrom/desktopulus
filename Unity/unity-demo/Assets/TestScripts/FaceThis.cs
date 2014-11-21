using UnityEngine;
using System.Collections;

public class FaceThis : MonoBehaviour {

	public Transform target;

	void Update () {
		Vector3 tar = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation(-tar);

	}
	
}
