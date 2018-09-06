using System.Collections;
using UnityEngine;
using VRM;

public class AssetBundleLoader : MonoBehaviour
{
	[SerializeField] public Object AllowedUserWarning, ViolenceWarning, SextialWarning, CommartialWarning;
	public IEnumerator LoadAssetBundle (string path, GameObject currentModel)
	{
		string lower8 = path.Substring (path.Length - 8).ToLower ();
		if (lower8 == "manifest")
		{
			path = path.Substring (0, path.Length - 9);
		}
		Debug.Log (path);

		var resultAssetbundle = AssetBundle.LoadFromFileAsync (path);

		yield return new WaitWhile (()=> resultAssetbundle.isDone == false);
		var assetbundle = resultAssetbundle.assetBundle;

		foreach (string name in assetbundle.GetAllAssetNames ())
		{
			if (name.Substring (name.Length - 7)== ".prefab")
			{
				char[] slash = new char[] { '/' };
				var objName = name.Split (slash);
				string s = objName[objName.Length - 1].Substring (0, objName[objName.Length - 1].Length - 7);

				var resultObject = assetbundle.LoadAssetAsync<GameObject> (s);
				yield return new WaitWhile (()=> resultObject.isDone == false);

				var obj = (GameObject)Instantiate (resultObject.asset);
				if (obj.GetComponent<Animator> ())
				{
					currentModel.GetComponent<Animator> ().runtimeAnimatorController = obj.GetComponent<Animator> ().runtimeAnimatorController;
					GameObject.FindObjectOfType<ClickBoneObserver> ().anim = obj.GetComponent<Animator> ();
				}
				if (obj.GetComponent<Settings4VRM> ())
				{
					LicenseCheck (currentModel, obj.GetComponent<Settings4VRM> ());
				}
			}
		}
		assetbundle.Unload (false);
	}
	void LicenseCheck (GameObject vrmModel, Settings4VRM setting)
	{
		VRMMetaObject meta = vrmModel.GetComponent<VRMMeta> ().Meta;
		if (meta.AllowedUser != AllowedUser.Everyone)
		{
			Destroy (vrmModel);
			Instantiate (AllowedUserWarning);
			return;
		}
		if (meta.ViolentUssage == UssageLicense.Disallow && setting.violent == UssageLicense.Allow)
		{
			Destroy (vrmModel);
			Instantiate (ViolenceWarning);
			return;
		}
		if (meta.ViolentUssage == UssageLicense.Disallow && setting.sextiality == UssageLicense.Allow)
		{
			Destroy (vrmModel);
			Instantiate (SextialWarning);
			return;
		}
		if (meta.ViolentUssage == UssageLicense.Disallow && setting.commercial == UssageLicense.Allow)
		{
			Destroy (vrmModel);
			Instantiate (CommartialWarning);
			return;
		}
	}
}