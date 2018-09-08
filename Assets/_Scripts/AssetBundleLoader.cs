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
				if (obj.GetComponent<Settings4VRM> ())
				{
					LicenseCheck (currentModel, obj.GetComponent<Settings4VRM> ());
				}
			}
			if (name.Substring (name.Length - 11)== ".controller")
			{
				char[] slash = new char[] { '/' };
				var objName = name.Split (slash);
				string s = objName[objName.Length - 1].Substring (0, objName[objName.Length - 1].Length - 11);

				var resultObject = assetbundle.LoadAsset (s)as RuntimeAnimatorController;

				if (currentModel.GetComponent<Animator> ())
				{
					currentModel.GetComponent<Animator> ().runtimeAnimatorController = resultObject;
					FindObjectOfType<ClickBoneObserver> ().anim = currentModel.GetComponent<Animator> ();
					FindObjectOfType<HumanCollider> ().Adjust4Model ();
				}
			}
		}
		assetbundle.Unload (false);
	}
	void LicenseCheck (GameObject vrmModel, Settings4VRM setting)
	{
		VRMMeta meta = vrmModel.GetComponent<VRMMeta> ();
		if (meta)
		{
			VRMMetaObject metaObj = meta.Meta;
			if (metaObj.AllowedUser != AllowedUser.Everyone)
			{
				Destroy (vrmModel);
				GameObject obj = (GameObject)Instantiate (AllowedUserWarning);
				obj.transform.position = new Vector3 (0, 1, -1.5f);
				obj.transform.rotation = Quaternion.Euler (0, 90, 0);
				FindObjectOfType<VRMAnimLoader> ().currentModel = obj;
				return;
			}
			if (metaObj.ViolentUssage == UssageLicense.Disallow && setting.violent == UssageLicense.Allow)
			{
				Destroy (vrmModel);
				GameObject obj = (GameObject)Instantiate (ViolenceWarning);
				obj.transform.position = new Vector3 (0, 1, -1.5f);
				obj.transform.rotation = Quaternion.Euler (0, 90, 0);
				FindObjectOfType<VRMAnimLoader> ().currentModel = obj;

				return;
			}
			if (metaObj.SexualUssage == UssageLicense.Disallow && setting.sexuality == UssageLicense.Allow)
			{
				Destroy (vrmModel);
				GameObject obj = (GameObject)Instantiate (SextialWarning);
				obj.transform.position = new Vector3 (0, 1, -1.5f);
				obj.transform.rotation = Quaternion.Euler (0, 90, 0);
				FindObjectOfType<VRMAnimLoader> ().currentModel = obj;

				return;
			}
			if (metaObj.CommercialUssage == UssageLicense.Disallow && setting.commercial == UssageLicense.Allow)
			{
				Destroy (vrmModel);
				GameObject obj = (GameObject)Instantiate (CommartialWarning);
				obj.transform.position = new Vector3 (0, 1, -1.5f);
				obj.transform.rotation = Quaternion.Euler (0, 90, 0);
				FindObjectOfType<VRMAnimLoader> ().currentModel = obj;
				return;
			}
		}
		return;
	}
}