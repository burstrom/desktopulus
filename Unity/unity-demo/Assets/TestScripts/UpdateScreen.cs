using UnityEngine;
using System.Collections;
using System.Drawing.Imaging;
using System.Windows.Forms;




public class UpdateScreen : MonoBehaviour {

	private static System.Drawing.Bitmap bmpScreenshot;
	private static System.Drawing.Graphics gfxScreenshot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Use System.Window.Forms to access the screen you want to capture. Capture a bitmap of that screen.
		bmpScreenshot = new System.Drawing.Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
		gfxScreenshot = System.Drawing.Graphics.FromImage(bmpScreenshot);

		gfxScreenshot.CopyFromScreen(System.Windows.Forms.Screen.PrimaryScreen.Bounds.X,
		                             System.Windows.Forms.Screen.PrimaryScreen.Bounds.Y,
		                             0,
		                             0,
		                             System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size,
		                             System.Drawing.CopyPixelOperation.SourceCopy);
		//Convert the bitmap image into an "actual" image.
		bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);
		
		
		// "Download" the screenshot.
		var www = new WWW("file://Screenshot.png");
			
			
		//Apply the screenshot as a texture, replacing the current one.
		renderer.material.mainTexture = www.texture;

	}
}
