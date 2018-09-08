/**
 * VrmSample
 * 
 * Author: Kirurobo http://twitter.com/kirurobo
 * License: CC0 https://creativecommons.org/publicdomain/zero/1.0/
 */

using System;
using System.Collections;
using System.IO;
using UniHumanoid;
using UnityEngine;
using VRM;

public class VRMAnimLoader : MonoBehaviour
{
	private WindowController windowController;

	private VRMImporterContext context;
	private VRMMetaObject meta;

	public CameraController cameraController;
	public Transform cameraTransform;

	private CameraController.WheelMode originalWheelMode;
	[SerializeField] public GameObject currentModel;
	AssetBundleLoader loader;

	// Use this for initialization
	void Start ()
	{
		if (!cameraController)
		{
			cameraController = FindObjectOfType<CameraController> ();
			if (cameraController)
			{
				originalWheelMode = cameraController.wheelMode;
			}
		}

		// Initialize window manager
		windowController = FindObjectOfType<WindowController> ();
		if (windowController)
		{
			// Add a file drop handler.
			windowController.OnFilesDropped += Window_OnFilesDropped;
		}
		loader = FindObjectOfType<AssetBundleLoader> ();
	}

	void Update ()
	{
		// ホイール操作は不透明なところでのみ受け付けさせる
		if (windowController && cameraController)
		{
			Vector2 pos = Input.mousePosition;
			bool inScreen = (pos.x >= 0 && pos.x < Screen.width && pos.y >= 0 && pos.y < Screen.height);
			if (windowController.isFocusable && inScreen)
			{
				cameraController.wheelMode = originalWheelMode;
			}
			else
			{
				cameraController.wheelMode = CameraController.WheelMode.None;
			}
		}
	}

	private void Window_OnFilesDropped (string[] files)
	{
		foreach (string path in files)
		{
			string ext = path.Substring (path.Length - 4).ToLower ();

			// Open the VRM file if its extension is ".vrm".
			if (ext == ".vrm")
			{
				LoadModel (path);
				break;
			}
			string lower8 = path.Substring (path.Length - 8).ToLower ();
			if (lower8 == ".unity3d" || lower8 == "manifest")
			{
				StartCoroutine (loader.LoadAssetBundle (path, currentModel));
				break;
			}
		}
	}

	private void LoadModel (string path)
	{
		if (!File.Exists (path))return;

		GameObject newModel = null;

		try
		{
			byte[] bytes = File.ReadAllBytes (path);
			context = new VRMImporterContext (UniGLTF.UnityPath.FromFullpath (path));
			context.ParseGlb (bytes);
			VRMImporter.LoadFromBytes (context);

			newModel = context.Root;
			meta = context.ReadMeta ();
			if (currentModel.GetComponent<Animator> ())
			{
				newModel.GetComponent<Animator> ().runtimeAnimatorController = currentModel.GetComponent<Animator> ().runtimeAnimatorController;
				FindObjectOfType<ClickBoneObserver> ().anim = newModel.GetComponent<Animator> ();
				FindObjectOfType<HumanCollider> ().Adjust4Model ();
			}
			Destroy (currentModel);
			currentModel = newModel;
		}
		catch (Exception ex)
		{
			Debug.LogError (ex);
			return;
		}
	}

}