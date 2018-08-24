using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UnityEngine;

public class TransparentForm : Form
{
	RenderTexture tex;
	public System.Drawing.Bitmap Image2paint;
	Point mousePoint;
	public TransparentForm (RenderTexture Rendertex, Camera main)
	{
		tex = Rendertex;
		main.targetTexture = tex;

		this.SetStyle (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
		this.SetBounds (0, 0, tex.width, tex.height);

		this.BackColor = System.Drawing.Color.Black;
		this.TransparencyKey = System.Drawing.Color.Black;
		this.FormBorderStyle = FormBorderStyle.None;
		this.TopMost = true;

		this.MouseDown += new MouseEventHandler (Form1_MouseDown);
		this.MouseMove += new MouseEventHandler (Form1_MouseMove);

		this.Show ();
	}
	protected override void OnPaint (PaintEventArgs e)
	{
		base.OnPaint (e);
		e.Graphics.DrawImage (Image2paint, 0, 0, tex.width, tex.height);
		this.Invalidate ();
	}
	private void Form1_MouseDown (object sender,
		System.Windows.Forms.MouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left)== MouseButtons.Left)
		{
			mousePoint = new Point (e.X, e.Y);
		}
	}

	private void Form1_MouseMove (object sender,
		System.Windows.Forms.MouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left)== MouseButtons.Left)
		{
			this.Left += e.X - mousePoint.X;
			this.Top += e.Y - mousePoint.Y;
		}
	}
}