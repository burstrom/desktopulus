using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

public class DemoGifLoader : MonoBehaviour {

	public static List<List<Texture2D>> gFrames = new List<List<Texture2D>>();
	
	void Start () {
		for(int j = 0; j<9; j++){
		List<Texture2D> gifFrames = new List<Texture2D>();
		gFrames.Add(gifFrames);
			string loadingGifPath = Application.dataPath + "/TestScripts/Materials/"+ j +".gif";
			var gifImage = Image.FromFile (loadingGifPath);
			var dimension = new FrameDimension (gifImage.FrameDimensionsList [0]);
			int frameCount = gifImage.GetFrameCount (dimension);
			for (int i = 0; i < frameCount; i++) {
				gifImage.SelectActiveFrame (dimension, i);
				var frame = new Bitmap (gifImage.Width, gifImage.Height);
				System.Drawing.Graphics.FromImage (frame).DrawImage (gifImage, Point.Empty);
				Texture2D frameTexture = new Texture2D (frame.Width, frame.Height);
				for (int x = 0; x < frame.Width; x++)
				for (int y = 0; y < frame.Height; y++) {
					System.Drawing.Color sourceColor = frame.GetPixel (x, y);
					frameTexture.SetPixel (x, frame.Height - 1 - y, new Color32 (sourceColor.R, sourceColor.G, sourceColor.B, sourceColor.A)); // for some reason, x is flipped
				}
				frameTexture.Apply ();
				gFrames[j].Add(frameTexture);
				//gifFrames.Add (frameTexture);
			}
		}
	}
}
