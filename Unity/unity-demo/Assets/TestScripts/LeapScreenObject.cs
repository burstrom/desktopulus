﻿using UnityEngine;
using System.Collections;
using Leap;

//Leap part of the screen objects. Extends LeapGameObject and is based on the Leap Basic Object.
//We can access the manipulated item by using "base" as in for example base.transform.localScale.

[RequireComponent(typeof(Rigidbody))]
public class LeapScreenObject : LeapGameObject{


	public override LeapState Activate(HandTypeBase h)
	{
		if (owner != null){
			return null;
		}
		
		if (h.unityHand.isRightHand){
			Debug.Log ("RIGHT");
		} else {
			Debug.Log ("LEFT");
		}
		//hmm, I always detect all fingers? unityFingers are not the fingers you're looking for.
		//foreach (var finger in h.unityHand.unityFingers){
		//	Debug.Log (finger);
		//}
		//We can detect the amount of fingers? Doing a pinching motion seems to trigger as "1" finger. Lets try it.
		//Debug.Log("The amount of fingers detected is: " + h.unityHand.hand.Fingers.Count);

		
		if (canGoThroughGeometry && rigidbody)
		{
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;
		}
		return base.Activate(h);
	}
	

	
	
	//What happens when you release the object.
	public override LeapState Release(HandTypeBase h)
	{
		LeapState state = null;
		
		if (!isStatePersistent)
		{
		
			if (canGoThroughGeometry && rigidbody)
			{
				rigidbody.isKinematic = false;
				rigidbody.useGravity = false;
			}
			//Just to test if it worked, this is to be put in the "pinch drag" action. Or rather a version that does the size change
			//Dynamically
			//base.transform.localScale += new Vector3(2.0f, 2.0f, 2.0f);
			state = base.Release(h);
		}
		
		return state;
	}
	
	public override void UpdateTransform(HandTypeBase t)
	{	
		//Only update translation if you are doing a full grab. If any fingers are detected, assume you are trying to do a gesture.
		if(t.unityHand.hand.Fingers.Count == 0){
			base.UpdateTransform(t);
		}
		
		//For each gesture detected.
		foreach (Gesture g in LeapInputEx.Controller.Frame().Gestures())
		{	
			//Check if the gesture was a circle
			if (g.Type == Gesture.GestureType.TYPECIRCLE)
			{
				Debug.Log ("CIRCLE DETECTED!");
				
				Debug.Log ("a: " + g.Type);
				Debug.Log ("b: " + Gesture.GestureType.TYPECIRCLE);
				 
				//Debug.Log (g.Normal as CircleGesture);
				//Debug.Log(circle.Normal);

//				Debug.Log(g.Pointables[0].Direction.AngleTo(c.Normal));
//				if (g.Pointables[0].Direction.AngleTo(c.Normal) <= Mathf.PI/2) {
//					string clockwiseness = "clockwise detected, increasing size.";
//					base.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
//					
//					Debug.Log (clockwiseness);
//				}
//				else
//				{
//					string clockwiseness = "counterclockwise detected, decreasing size.";
//					base.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
//					
//					Debug.Log (clockwiseness);
//				}
//				//Normally we would want to use AngleTo to compare the pointable vector to the NORMAL of the gesture.
//				//However for some reason I can't access this variable in unity. So I do it by basically checking against the Yaw of
//				//the hand doing the gesture. This is incredibly buggy atm...
//				if (g.Pointables[0].Hand.PalmNormal.Yaw < 0.0f) {
//					string clockwiseness = "clockwise detected, increasing size.";
//					base.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
//					
//					Debug.Log (clockwiseness);
//				}
//				else
//				{
//					string clockwiseness = "counterclockwise detected, decreasing size.";
//					base.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
//					
//					Debug.Log (clockwiseness);
//				}
				
				
			}
		}
		
		if (!rigidbody.isKinematic)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			
			// Necessary to switch Hand Updates for collisions
			if (collisionOccurred)
			{
				owner.unityHand.runUpdate = false;
			}
			else
			{
				owner.unityHand.runUpdate = true;
			}
		}
	}

}
