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
	[SerializeField] public GameObject currentModel;
	AssetBundleLoader loader;

	void Start()
	{
		// Initialize window manager
		windowController = FindObjectOfType<WindowController>();
		if (windowController)
		{
			// Add a file drop handler.
			windowController.OnFilesDropped += Window_OnFilesDropped;
		}
		loader = FindObjectOfType<AssetBundleLoader>();
	}

	private void Window_OnFilesDropped(string[] files)
	{
		foreach (string path in files)
		{
			// Open the VRM file if its extension is ".vrm".
			if (StringUtil.TailMatch(path, ".vrm"))
			{
				LoadModel(path);
				break;
			}
			if (StringUtil.TailMatch(path, ".unity3d")|| StringUtil.TailMatch(path, "manifest"))
			{
				StartCoroutine(loader.LoadAssetBundle(path, currentModel));
				break;
			}
		}
	}

	private void LoadModel(string path)
	{
		if (!File.Exists(path))return;

		GameObject newModel = null;
		try
		{
			byte[] bytes = File.ReadAllBytes(path);
			context = new VRMImporterContext(UniGLTF.UnityPath.FromFullpath(path));
			context.ParseGlb(bytes);
			VRMImporter.LoadFromBytes(context);

			newModel = context.Root;
			meta = context.ReadMeta();
			if (currentModel.GetComponent<Animator>())
			{
				newModel.GetComponent<Animator>().runtimeAnimatorController = currentModel.GetComponent<Animator>().runtimeAnimatorController;
				FindObjectOfType<ClickBoneObserver>().anim = newModel.GetComponent<Animator>();
				FindObjectOfType<HumanCollider>().Adjust4Model();
			}
			Destroy(currentModel);
			currentModel = newModel;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			return;
		}
	}
}