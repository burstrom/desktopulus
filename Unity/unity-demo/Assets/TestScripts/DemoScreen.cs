using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine;

public class DemoScreen : MonoBehaviour
{
		public string loadingGifPath = "/Assets/TestScripts/Material/owls-yousrs.gif";
		public float speed = 1;
		private int frame = 0;
		public int framecount = 0;
		private int count = 0;
		public Vector2 drawPosition;
		private Texture2D frameTexture;
	
		List<Texture2D> gifFrames = new List<Texture2D> ();
		void Start ()
		{
				var gifImage = Image.FromFile (loadingGifPath);
				var dimension = new FrameDimension (gifImage.FrameDimensionsList [0]);
				int frameCount = gifImage.GetFrameCount (dimension);
				for (int i = 0; i < frameCount; i++) {
						gifImage.SelectActiveFrame (dimension, i);
						var frame = new Bitmap (gifImage.Width, gifImage.Height);
						System.Drawing.Graphics.FromImage (frame).DrawImage (gifImage, Point.Empty);
						frameTexture = new Texture2D (frame.Width, frame.Height);
						for (int x = 0; x < frame.Width; x++)
								for (int y = 0; y < frame.Height; y++) {
										System.Drawing.Color sourceColor = frame.GetPixel (x, y);
										frameTexture.SetPixel (x, frame.Height - 1 - y, new Color32 (sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
								}
						frameTexture.Apply ();
						gifFrames.Add (frameTexture);
				}
		}
	
		void Update ()
		{				
				renderer.material.mainTexture = gifFrames [frame];
				if (count != framecount) {
						count++;
				} else {
						frame++;
						if (frame == gifFrames.Count) {
								frame = 0;
						}
						count = 0;
				}
		}
}