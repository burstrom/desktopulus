using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;

public class DemoScreen : MonoBehaviour
{	
		
		//private string loadingGifPath = Application.dataPath + "/TestScripts/Materials/owls-yousrs.gif";
		public float speed = 1;
		private int frame = 0;
		public int framecount = 0;
		private int count = 0;
		public Vector2 drawPosition;
		private Texture2D frameTexture;
		private int snum; 
		void Start(){
			snum = GetComponent<updateObject>().screenNum;
		}
	
		void Update ()
		{				
			renderer.material.mainTexture = DemoGifLoader.gFrames[snum % 9][frame];
			if (count != framecount) {
				count++;
			} else {
				frame++;
				if (frame == DemoGifLoader.gFrames[snum % 9].Count) {
						frame = 0;
				}
				count = 0;
			}
		}
}