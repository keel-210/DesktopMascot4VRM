using UnityEngine;
using System;
using System.Runtime.InteropServices;
using DesktopMascotMaker;

// If you want to hide Unity's main window, attach this script and change Main Window Opacity value to 0.
// You can attach this scirpt anywhere in your scene.
//  If set Opacity to 0, main window will be disappeared.
//  If set Opacity to [1-254], main window will be transparented.
//  If set Opacity to 255, nothing happens.
// Note : This script only works in build(released) programs.

[AddComponentMenu("DesktopMascotMaker/MainWindowOpacity")]
public class MainWindowOpacity : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern Boolean SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    const int LWA_ALPHA = 0x2;
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;

    public int Opacity = 255;

    void Awake() { }

    void Start()
    {
        SetMainWindowOpacity(Opacity);
    }

    public void SetMainWindowOpacity(int MainWindowOpacity)
    {
        Opacity = MainWindowOpacity;

#if UNITY_EDITOR

#else
        if (MascotMaker.MainWindowHandle != IntPtr.Zero)
        {
            if (Opacity >= 255) Opacity = 255;
            if (Opacity <= 0) Opacity = 0;

            int extStyle = GetWindowLong(MascotMaker.MainWindowHandle, GWL_EXSTYLE);
            SetWindowLong(MascotMaker.MainWindowHandle, GWL_EXSTYLE, extStyle | WS_EX_LAYERED);
            SetLayeredWindowAttributes(MascotMaker.MainWindowHandle, 0, (byte)Opacity, LWA_ALPHA);
        }
        else if (MascotMakerMulti.MainWindowHandle != IntPtr.Zero)
        {
            if (Opacity >= 255) Opacity = 255;
            if (Opacity <= 0) Opacity = 0;

            int extStyle = GetWindowLong(MascotMakerMulti.MainWindowHandle, GWL_EXSTYLE);
            SetWindowLong(MascotMakerMulti.MainWindowHandle, GWL_EXSTYLE, extStyle | WS_EX_LAYERED);
            SetLayeredWindowAttributes(MascotMakerMulti.MainWindowHandle, 0, (byte)Opacity, LWA_ALPHA);
        }
#endif
    }
}
