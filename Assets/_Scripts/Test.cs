using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class Test : Form
{
	protected override void OnPaint (PaintEventArgs e)
	{
		base.OnPaint (e);
		GraphicsPath path = new GraphicsPath ();
		path.AddEllipse (0, 0, ClientSize.Width, ClientSize.Height);

		Region region = new Region (ClientRectangle);
		region.Xor (path);

		Brush brush = new SolidBrush (Color.White);
		e.Graphics.FillRegion (brush, region);
	}

	public static void Main ()
	{
		Form form = new Test ();
		form.TransparencyKey = form.BackColor;
		Application.Run (form);
	}
}