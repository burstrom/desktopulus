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
		private int screenNum;
		
	
		
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{		
				Debug.Log("AllScreens.Length: " + System.Windows.Forms.Screen.AllScreens.Length);
				Debug.Log("MonitorCount: " + System.Windows.Forms.SystemInformation.MonitorCount);
				// Use System.Window.Forms to access the screen you want to capture. Capture a bitmap of that screen.
				// XXXX For some reason, AllScreens always only has one object in it. 
				try {
						screenNum = GetComponent<updateObject>().screenNum;
						width = System.Windows.Forms.Screen.AllScreens[screenNum].WorkingArea.Width;
						height = System.Windows.Forms.Screen.AllScreens[screenNum].WorkingArea.Height;
				
						//Create the bitmap that will store the information from the screenbuffer.
						bmpScreenshot = new System.Drawing.Bitmap (width, height, PixelFormat.Format32bppArgb);
			               
			            //Create the graphics item that will perform the copy.                                                         
						gfxScreenshot = System.Drawing.Graphics.FromImage (bmpScreenshot);
	
						//Copy the screen into the bitmap.
						gfxScreenshot.CopyFromScreen (System.Windows.Forms.Screen.AllScreens[screenNum].WorkingArea.X,
			                             System.Windows.Forms.Screen.AllScreens[screenNum].WorkingArea.Y,
			                             0,
			                             0,
			                             System.Windows.Forms.Screen.AllScreens[screenNum].WorkingArea.Size,
			                             System.Drawing.CopyPixelOperation.SourceCopy);
			
						gfxScreenshot.Dispose ();
						
						//Create a bytearray, we can't convert a bitmap directly into a texture. But we can flush it into a byte array that can then
						//be converted to a texture.
						byte[] barr;
			            
			            //access the memory stream and flush it into the byte array.
						memStream = new System.IO.MemoryStream ();
						bmpScreenshot.Save (memStream, System.Drawing.Imaging.ImageFormat.Png);
						memStream.Close();
						bmpScreenshot.Dispose();
						barr = memStream.ToArray();
			
						//Create the new texture.
						var texture = new Texture2D (width, height, TextureFormat.ARGB32, false);
						
						//Destroy the old texture, will cause a memory leak otherwise.  
						if (renderer.material.mainTexture != null) {
								DestroyImmediate (renderer.material.mainTexture, true);
						}
						
						//Set the new texture as the main one.
						texture.LoadImage(barr);
						renderer.material.mainTexture = texture;
			
				} catch (System.IndexOutOfRangeException e) {
						Debug.Log ("Can not update screen, has no valid input:" + e);
				}
				
		}
	
}
