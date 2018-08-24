using UnityEngine;
using DesktopMascotMaker;

public class DMMUnityChanController : MonoBehaviour
{
    // If you want to use this script with MascotMakerMulti,
    // Uncomment the following line and replace 'MascotMaker.Instance' to 'mascotMakerMulti'.
    //public MascotMakerMulti mascotMakerMulti; // Assign MascotMakerMulti's instance to this variable.

    public DMMUnityChanAI unityChanAI = null;

    public TextMesh hoverCheck = null;

    void Start()
    {
        if (unityChanAI == null)
            Debug.LogError("UnityChanAI is not assigned!", transform);
    }

    void Update()
    {
        // Check whether cursor is over the mascot
        if (MascotMaker.Instance.IsMouseHover)
        {
            hoverCheck.color = Color.red;
            hoverCheck.text = "HOVER!";
        }
        else
        {
            hoverCheck.color = Color.black;
            hoverCheck.text = "NOT HOVER";
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 30), "Show 100%"))
        {
            // Show mascot and make it opacity 255.
            MascotMaker.Instance.Show();
            MascotMaker.Instance.Opacity = 255;
        }
        if (GUI.Button(new Rect(10, 40, 150, 30), "Show 50%"))
        {
            // Show mascot and make it half transparent.
            MascotMaker.Instance.Show();
            MascotMaker.Instance.Opacity = 128;
        }
        if (GUI.Button(new Rect(10, 70, 150, 30), "Hide"))
        {
            // Hide mascot.
            MascotMaker.Instance.Hide();
        }

        if (GUI.Button(new Rect(10, 130, 150, 30), "Walk"))
        {
            // Make UnityChan walk.
            unityChanAI.DoWalk();
        }
        if (GUI.Button(new Rect(10, 160, 150, 30), "Idle"))
        {
            // Make UnityChan idle.
            unityChanAI.DoIdle();
        }
    }
}
