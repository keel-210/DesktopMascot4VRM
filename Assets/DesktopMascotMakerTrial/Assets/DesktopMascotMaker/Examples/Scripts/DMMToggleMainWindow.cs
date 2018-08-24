using System.Collections;
using System.Windows.Forms;
using DesktopMascotMaker;
using UnityEngine;

public class DMMToggleMainWindow : MonoBehaviour
{
	// If you want to use this script with MascotMakerMulti,
	// Uncomment the following line and replace 'MascotMaker.Instance' to 'mascotMakerMulti'.
	//public MascotMakerMulti mascotMakerMulti; // Assign MascotMakerMulti's instance to this variable.

	public MainWindowOpacity mainWindowOpacity;

	void Start ()
	{
		// Add event handler
		//		MascotMaker.Instance.OnLeftMouseDown += LeftMouseDown;
	}

	public void LeftMouseDown (object sender, MouseEventArgs e)
	{
		if (mainWindowOpacity != null && MascotMaker.Instance.IsMouseHover)
		{
			if (mainWindowOpacity.Opacity != 0)
			{
				// Hide Unity's main window
				mainWindowOpacity.SetMainWindowOpacity (0);
			}
			else
			{
				// Show Unity's main window
				mainWindowOpacity.SetMainWindowOpacity (255);
			}
		}
	}
}