using System.Drawing;
using System.IO;
using OpenCvSharp;
using UnityEngine;
using UnityEngine.UI;

public class FormCaller : MonoBehaviour
{
	[SerializeField] Vector2 Size;
	[SerializeField] RenderTexture renderTexture;
	[SerializeField] Camera mainCam;
	[SerializeField] RawImage im;
	System.Drawing.Bitmap Image2paint;
	TransparentForm f;
	void Start ()
	{
		f = new TransparentForm (renderTexture, mainCam);
	}
	void OnPostRender ()
	{
		Texture2D Tex2Paint = new Texture2D (renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
		Tex2Paint.ReadPixels (new Rect (0, 0, renderTexture.width, renderTexture.height), 0, 0, false);
		//UnityEngine.Graphics.CopyTexture (renderTexture, Tex2Paint);
		im.texture = Tex2Paint;
		IplImage imIpl;

		Tex2Paint.Apply ();
		var img = Tex2Paint.EncodeToPNG ();

		MemoryStream memoryStream = new MemoryStream ();
		memoryStream.Write (img, 0, img.Length);

		f.Image2paint = new System.Drawing.Bitmap (memoryStream);
	}
	void OnDisable ()
	{
		f.Close ();
	}
}