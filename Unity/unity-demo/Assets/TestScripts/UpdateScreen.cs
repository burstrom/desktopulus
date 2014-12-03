using UnityEngine;
using System.Collections;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;



public class UpdateScreen : MonoBehaviour
{

		private static System.Drawing.Bitmap bmpScreenshot;
		private static System.Drawing.Graphics gfxScreenshot;
		private System.IO.MemoryStream memStream;
		private int height;
		private int width;
	
		
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				// Use System.Window.Forms to access the screen you want to capture. Capture a bitmap of that screen.
				//Right now we capture the primary screen all the time but using AllScreens[i] we can access all the screens.
				try {
						width = System.Windows.Forms.Screen.AllScreens [GetComponent<updateObject> ().screenNum].Bounds.Width;
						height = System.Windows.Forms.Screen.AllScreens [GetComponent<updateObject> ().screenNum].Bounds.Height;
				
						bmpScreenshot = new System.Drawing.Bitmap (width, height, 
			                                          PixelFormat.Format32bppArgb);
			                                          
						gfxScreenshot = System.Drawing.Graphics.FromImage (bmpScreenshot);
	
						gfxScreenshot.CopyFromScreen (System.Windows.Forms.Screen.AllScreens [GetComponent<updateObject> ().screenNum].Bounds.X,
			                             System.Windows.Forms.Screen.AllScreens [GetComponent<updateObject> ().screenNum].Bounds.Y,
			                             0,
			                             0,
			                             System.Windows.Forms.Screen.AllScreens [GetComponent<updateObject> ().screenNum].Bounds.Size,
			                             System.Drawing.CopyPixelOperation.SourceCopy);
						//The following two steps, we want to skip, basically we want to apply the texture directly wihtout saving it first.
			
						//Convert the bitmap image into an "actual" image.
						gfxScreenshot.Dispose ();
						byte[] barr;
			            
						
						memStream = new System.IO.MemoryStream ();
						bmpScreenshot.Save (memStream, System.Drawing.Imaging.ImageFormat.Png);
						memStream.Close();
						barr = memStream.ToArray();
			
			
						//Get rid of the screenshots from memory.
						bmpScreenshot.Dispose ();
						var texture = new Texture2D (width, height, TextureFormat.ARGB32, false); 
						if (renderer.material.mainTexture != null) {
								DestroyImmediate (renderer.material.mainTexture, true);
						}
						texture.LoadImage(barr);
						renderer.material.mainTexture = texture;
			
				} catch (System.IndexOutOfRangeException e) {
						Debug.Log ("Can not update screen, has no valid input:" + e);
				}
				
		}
	
}
