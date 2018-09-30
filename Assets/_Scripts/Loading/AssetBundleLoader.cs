using System.Collections;
using UnityEngine;
using VRM;

public class AssetBundleLoader : MonoBehaviour
{
	[SerializeField] public Object AllowedUserWarning, ViolenceWarning, SextialWarning, CommartialWarning;
	public IEnumerator LoadAssetBundle(string path, GameObject currentModel)
	{
		if (StringUtil.TailMatch(path, "manifest"))
		{
			path = path.Substring(0, path.Length - 9);
		}
		Debug.Log(path);

		var resultAssetbundle = AssetBundle.LoadFromFileAsync(path);

		yield return new WaitWhile(()=> resultAssetbundle.isDone == false);
		var assetbundle = resultAssetbundle.assetBundle;

		foreach (string name in assetbundle.GetAllAssetNames())
		{
			if (StringUtil.TailMatch(name, ".prefab"))
			{
				StartCoroutine(LoadPrefab(assetbundle, name, currentModel));
			}
			if (StringUtil.TailMatch(name, ".controller"))
			{
				LoadAnimatorController(assetbundle, name, currentModel);
			}
		}
		assetbundle.Unload(false);
	}
	IEnumerator LoadPrefab(AssetBundle assetbundle, string name, GameObject currentModel)
	{
		string s = StringUtil.FileName(name);
		var resultObject = assetbundle.LoadAssetAsync<GameObject>(s);
		yield return new WaitWhile(()=> resultObject.isDone == false);

		var obj = (GameObject)Instantiate(resultObject.asset);

		LicenseCheck(currentModel, obj.GetComponent<Settings4VRM>());
		if (s == "Menu")
		{
			Destroy(GameObject.Find("Canvas").transform.Find("Menu"));
			obj.transform.parent = GameObject.Find("Canvas").transform;
		}
	}
	void LoadAnimatorController(AssetBundle assetbundle, string name, GameObject currentModel)
	{
		string s = StringUtil.FileName(name);
		var resultObject = assetbundle.LoadAsset<RuntimeAnimatorController>(s);

		if (currentModel.GetComponent<Animator>())
		{
			currentModel.GetComponent<Animator>().runtimeAnimatorController = resultObject;
			FindObjectOfType<ClickBoneObserver>().anim = currentModel.GetComponent<Animator>();
			FindObjectOfType<HumanCollider>().Adjust4Model();
		}
	}
	void LicenseCheck(GameObject vrmModel, Settings4VRM setting)
	{
		if (setting != null)return;
		VRMMeta meta = vrmModel.GetComponent<VRMMeta>();
		if (meta)
		{
			VRMMetaObject metaObj = meta.Meta;
			if (metaObj.AllowedUser != AllowedUser.Everyone)
			{
				WarningObjecs(vrmModel, AllowedUserWarning);
			}
			if (metaObj.ViolentUssage == UssageLicense.Disallow && setting.violent == UssageLicense.Allow)
			{
				WarningObjecs(vrmModel, ViolenceWarning);
			}
			if (metaObj.SexualUssage == UssageLicense.Disallow && setting.sexuality == UssageLicense.Allow)
			{
				WarningObjecs(vrmModel, SextialWarning);
			}
			if (metaObj.CommercialUssage == UssageLicense.Disallow && setting.commercial == UssageLicense.Allow)
			{
				WarningObjecs(vrmModel, CommartialWarning);
			}
		}
	}
	GameObject WarningObjecs(GameObject vrmModel, Object warning)
	{
		Destroy(vrmModel);
		GameObject obj = (GameObject)Instantiate(warning);
		obj.transform.position = new Vector3(0, 1, -1.5f);
		obj.transform.rotation = Quaternion.Euler(0, 90, 0);
		return obj;
	}
}