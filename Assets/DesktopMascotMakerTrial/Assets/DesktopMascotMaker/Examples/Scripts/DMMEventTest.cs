using System.Windows.Forms;
using DesktopMascotMaker;
using UnityEngine;

public class DMMEventTest : MonoBehaviour
{
    // If you want to use this script with MascotMakerMulti,
    // Uncomment the following line and replace 'MascotMaker.Instance' to 'mascotMakerMulti'.
    //public MascotMakerMulti mascotMakerMulti; // Assign MascotMakerMulti's instance to this variable.

    public DMMTextColorChanger tccLeftMouseDown;
    public DMMTextColorChanger tccLeftMouseUp;
    public DMMTextColorChanger tccLeftDoubleClick;
    public DMMTextColorChanger tccRightMouseDown;
    public DMMTextColorChanger tccRightMouseUp;
    public DMMTextColorChanger tccRightDoubleClick;
    public DMMTextColorChanger tccMiddleMouseDown;
    public DMMTextColorChanger tccMiddleMouseUp;
    public DMMTextColorChanger tccMiddleDoubleClick;
    public DMMTextColorChanger tccMouseWheel;
    public DMMTextColorChanger tccActivated;
    public DMMTextColorChanger tccDeactivate;
    public DMMTextColorChanger tccKeyDown;
    public DMMTextColorChanger tccKeyUp;

    void Start ()
    {
        // Assign custom function to MascotMaker's EventHandler

        /* // Click Events
        MascotMaker.Instance.OnLeftMouseDown += LeftMouseDown;
        MascotMaker.Instance.OnLeftMouseUp += LeftMouseUp;
        MascotMaker.Instance.OnLeftDoubleClick += LeftDoubleClick;
        MascotMaker.Instance.OnRightMouseDown += RightMouseDown;
        MascotMaker.Instance.OnRightMouseUp += RightMouseUp;
        MascotMaker.Instance.OnRightDoubleClick += RightDoubleClick;
        MascotMaker.Instance.OnMiddleMouseDown += MiddleMouseDown;
        MascotMaker.Instance.OnMiddleMouseUp += MiddleMouseUp;
        MascotMaker.Instance.OnMiddleDoubleClick += MiddleDoubleClick;

        // MouseWheel Event
        MascotMaker.Instance.OnMouseWheel += MouseWheel;

        // Activate/Deactivate Events
        MascotMaker.Instance.OnActivated += Activated;
        MascotMaker.Instance.OnDeactivate += Deactivate;

        // Key Events
        MascotMaker.Instance.OnKeyDown += KeyDown;
        MascotMaker.Instance.OnKeyUp += KeyUp; */
    }

    // Click Events
    public void LeftMouseDown (object sender, MouseEventArgs e)
    {
        tccLeftMouseDown.MakeTextRed ("LeftMouseDown! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void LeftMouseUp (object sender, MouseEventArgs e)
    {
        tccLeftMouseUp.MakeTextRed ("LeftMouseUp! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void LeftDoubleClick (object sender, System.EventArgs e)
    {
        tccLeftDoubleClick.MakeTextRed ("LeftDoubleClick!"); // X:" + e.X.ToString() + " Y:" + e.Y.ToString());
    }
    public void RightMouseDown (object sender, MouseEventArgs e)
    {
        tccRightMouseDown.MakeTextRed ("RightMouseDown! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void RightMouseUp (object sender, MouseEventArgs e)
    {
        tccRightMouseUp.MakeTextRed ("RightMouseUp! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void RightDoubleClick (object sender, MouseEventArgs e)
    {
        tccRightDoubleClick.MakeTextRed ("RightDoubleClick! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void MiddleMouseDown (object sender, MouseEventArgs e)
    {
        tccMiddleMouseDown.MakeTextRed ("MiddleMouseDown! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void MiddleMouseUp (object sender, MouseEventArgs e)
    {
        tccMiddleMouseUp.MakeTextRed ("MiddleMouseUp! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }
    public void MiddleDoubleClick (object sender, MouseEventArgs e)
    {
        tccMiddleDoubleClick.MakeTextRed ("MiddleDoubleClick! X:" + e.X.ToString ()+ " Y:" + e.Y.ToString ());
    }

    // MouseWheel Event
    void MouseWheel (object sender, MouseEventArgs e)
    {
        tccMouseWheel.MakeTextRed ("MouseWheel! Delta: " + e.Delta.ToString ());
    }

    // Activate/Deactivate Events
    public void Activated (object sender, System.EventArgs e)
    {
        tccActivated.MakeTextRed ("Activated " + MascotMaker.Instance.Title);
    }
    public void Deactivate (object sender, System.EventArgs e)
    {
        tccDeactivate.MakeTextRed ("Deactivate " + MascotMaker.Instance.Title);
    }

    // KeyEvents
    void KeyDown (object sender, KeyEventArgs e)
    {
        tccKeyDown.MakeTextRed ("KeyDown! KeyCode: " + e.KeyCode.ToString ());
    }
    void KeyUp (object sender, KeyEventArgs e)
    {
        tccKeyUp.MakeTextRed ("KeyUp! KeyCode: " + e.KeyCode.ToString ());
    }
}