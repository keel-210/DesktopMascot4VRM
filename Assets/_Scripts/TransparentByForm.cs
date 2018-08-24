using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UnityEngine;

public class TransparentByForm : MonoBehaviour
{
	[SerializeField] RenderTexture tex;
	[SerializeField] Camera mainCamera;
	System.Drawing.Bitmap Image2paint;
	Form form;
	System.Drawing.Graphics g;
	void Start ()
	{
		mainCamera.targetTexture = tex;
		form = new Form ();
		form.BackColor = System.Drawing.Color.Black;
		form.TransparencyKey = System.Drawing.Color.Black;
		form.TopMost = true;
		g = form.CreateGraphics ();
		form.Show ();
	}

	void OnPostRender ()
	{
		Texture2D Tex2Paint = new Texture2D (tex.width, tex.height);
		Tex2Paint.ReadPixels (new Rect (0, 0, tex.width, tex.height), 0, 0);
		Tex2Paint.Apply ();
		var img2Paint = new Bitmap (tex.width, tex.height);
		MemoryStream memoryStream = new MemoryStream ();
		var img = Tex2Paint.EncodeToPNG ();
		memoryStream.Write (img, 0, img.Length);

		Image2paint = new System.Drawing.Bitmap (memoryStream);
		if (Image2paint != null && g != null)
		{
			g.Clear (System.Drawing.Color.Black);
			g.DrawImage (Image2paint, 0, 0, tex.width, tex.height);
		}
	}
	void Update ()
	{
		if (form != null)
		{
			form.Show ();
		}
	}
}