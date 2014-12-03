using UnityEngine;
using System.Collections;
using System.Drawing.Imaging;
using System.Windows.Forms;




public class UpdateScreen : MonoBehaviour {

	private static System.Drawing.Bitmap bmpScreenshot;
	private static System.Drawing.Graphics gfxScreenshot;
	
	IEnumerator updateTexture(){
		WWW www = new WWW("file://Screenshot.png");
		yield return www;
		if (renderer.material.mainTexture != null)
			DestroyImmediate(renderer.material.mainTexture, true);
			
		renderer.material.mainTexture = www.texture;
		
		www.Dispose();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Use System.Window.Forms to access the screen you want to capture. Capture a bitmap of that screen.
		//Right now we capture the primary screen all the time but using AllScreens[i] we can access all the screens.
		try{
			bmpScreenshot = new System.Drawing.Bitmap(System.Windows.Forms.Screen.AllScreens[GetComponent<updateObject>().screenNum].Bounds.Width, 
			                                          System.Windows.Forms.Screen.AllScreens[GetComponent<updateObject>().screenNum].Bounds.Height, 
			                                          PixelFormat.Format32bppArgb);
			                                          
			gfxScreenshot = System.Drawing.Graphics.FromImage(bmpScreenshot);
	
			gfxScreenshot.CopyFromScreen(System.Windows.Forms.Screen.AllScreens[GetComponent<updateObject>().screenNum].Bounds.X,
			                             System.Windows.Forms.Screen.AllScreens[GetComponent<updateObject>().screenNum].Bounds.Y,
			                             0,
			                             0,
			                             System.Windows.Forms.Screen.AllScreens[GetComponent<updateObject>().screenNum].Bounds.Size,
			                             System.Drawing.CopyPixelOperation.SourceCopy);
			//The following two steps, we want to skip, basically we want to apply the texture directly wihtout saving it first.
			
			//Convert the bitmap image into an "actual" image.
			gfxScreenshot.Dispose();
			bmpScreenshot.Save("Screenshot.png", ImageFormat.Png);
			
			
			//Get rid of the screenshots from memory.
			bmpScreenshot.Dispose();
		} catch (System.IndexOutOfRangeException e){
			Debug.Log ("Can not update screen, has no valid input.");
		}
		
		
		StartCoroutine(updateTexture());
		
		
		
		
	}
	
}
