using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder
{
	[MenuItem ("Export/Assetbundle")]
	static void BuildAssetBundles ()
	{
		Directory.CreateDirectory (Application.streamingAssetsPath);
		BuildAssetBundleOptions options = BuildAssetBundleOptions.None; // 特になし
		BuildTarget target = EditorUserBuildSettings.activeBuildTarget; // 現在のビルドターゲットを取得
		BuildPipeline.BuildAssetBundles (Application.streamingAssetsPath, options, target);
	}
}