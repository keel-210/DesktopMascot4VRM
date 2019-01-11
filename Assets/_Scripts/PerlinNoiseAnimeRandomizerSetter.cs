//  PerlinNoiseAnimeRandomizerSetter.cs
//  http://kan-kikuchi.hatenablog.com/entry/PerlinNoise_Anime
//
//  Created by kan.kikuchi on 2018.12.01.

using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 子以下の全てのTransformにPerlinNoiseAnimeRandomizerを設定するクラス
/// </summary>
public class PerlinNoiseAnimeRandomizerSetter : MonoBehaviour
{

	[SerializeField, Header("↓設定したRandomizer")]
	private List<PerlinNoiseAnimeRandomizer> _randomizerList = new List<PerlinNoiseAnimeRandomizer>();

	[SerializeField, Header("↓設定しないTransformの親(設定したTransformとその子には設定しなくなる)")]
	private List<Transform> _ignoreParentList = new List<Transform>();

	[SerializeField, Header("↓設定しないTransform(設定したTransformだけ設定しなくなる)")]
	private List<Transform> _ignoreTransformList = new List<Transform>();

	[SerializeField, Header("↓全体のゆらぎの倍率(大きくすると全体的に大きく動く)")]
	private float _wholeScale = 10;

	[SerializeField, Header("↓XYZごとのゆらぎの倍率(大きくすると全体的に大きく動く)")]
	private Vector3 _xyzScale = Vector3.one;

	[SerializeField, Header("↓ゆらぎの速さ(大きくすると速く動く)")]
	private float _frequencyRate = 1f;

	//=================================================================================
	//初期化
	//=================================================================================

	private void Awake()
	{
		SetAllChildren();
	}

	//=================================================================================
	//設定
	//=================================================================================

	/// <summary>
	/// Transform以下全てにPerlinNoiseAnimeRandomizerを設定
	/// </summary>
	public void SetAllChildren()
	{
		SetRandomizer(transform);
	}

	//引数のTransformとそれ以下の子にPerlinNoiseAnimeRandomizerを設定する
	private void SetRandomizer(Transform targetTransform)
	{

		//設定しない親だったら終了、それ以下の子にも設定しない
		if (_ignoreParentList.Contains(targetTransform))
		{
			return;
		}

		//設定しないTransformか、SetterのTransformでない、PerlinNoiseAnimeRandomizerが付いていなければ設定
		if (!_ignoreTransformList.Contains(targetTransform) && targetTransform != transform && targetTransform.gameObject.GetComponent<PerlinNoiseAnimeRandomizer>() == null)
		{
			PerlinNoiseAnimeRandomizer randomizer = targetTransform.gameObject.AddComponent<PerlinNoiseAnimeRandomizer>();
			randomizer.SetStatus(_wholeScale, _xyzScale, _frequencyRate);
			_randomizerList.Add(randomizer);
		}

		//全ての子にも同様に設定
		for (int childNo = 0; childNo < targetTransform.childCount; childNo++)
		{
			SetRandomizer(targetTransform.GetChild(childNo));
		}

	}

	//=================================================================================
	//削除
	//=================================================================================

	/// <summary>
	/// Transform以下全てからPerlinNoiseAnimeRandomizerを削除
	/// </summary>
	public void RemoveAllChildren()
	{
		RemoveRandomizer(transform);
	}

	//引数のTransformとそれ以下の子からPerlinNoiseAnimeRandomizerを削除
	private void RemoveRandomizer(Transform targetTransform)
	{
#if UNITY_EDITOR
		//直で削除するとMissingReferenceExceptionのエラーが出るので間を開けて削除
		UnityEditor.EditorApplication.delayCall += () => { DestroyImmediate(targetTransform.gameObject.GetComponent<PerlinNoiseAnimeRandomizer>()); };
#endif
		//全ての子からも同様に削除
		for (int childNo = 0; childNo < targetTransform.childCount; childNo++)
		{
			RemoveRandomizer(targetTransform.GetChild(childNo));
		}
	}

	//=================================================================================
	//更新
	//=================================================================================

	/// <summary>
	/// 設定した全てのPerlinNoiseAnimeRandomizerのゆらぎの倍率を更新する
	/// </summary>
	public void UpdateScale()
	{
		foreach (PerlinNoiseAnimeRandomizer randomizer in _randomizerList)
		{
			if (randomizer != null)
			{
				randomizer.SetStatus(_wholeScale, _xyzScale, _frequencyRate);
			}
		}
	}

}

#if UNITY_EDITOR

/// <summary>
/// PerlinNoiseAnimeRandomizerSetterのInspectorを変えるエディター
/// </summary>
[CustomEditor(typeof(PerlinNoiseAnimeRandomizerSetter))]
public class PerlinNoiseAnimeRandomizerSetterEditor : Editor
{

	//=================================================================================
	//更新
	//=================================================================================

	//Inspectorを更新する
	public override void OnInspectorGUI()
	{
		//元の表示
		base.OnInspectorGUI();

		//更新開始
		serializedObject.Update();

		//PerlinNoiseAnimeRandomizerSetterを取得
		PerlinNoiseAnimeRandomizerSetter setter = target as PerlinNoiseAnimeRandomizerSetter;

		GUILayout.Space(30);
		GUILayout.Label("----------各種実行ボタン----------");

		//各ボタンを表示
		if (GUILayout.Button("子にRandomizerを設定"))
		{
			setter.SetAllChildren();
		}
		if (GUILayout.Button("子からRandomizerを削除"))
		{
			setter.RemoveAllChildren();
		}
		if (GUILayout.Button("設定したRandomizerの各種数値を更新"))
		{
			setter.UpdateScale();
		}

		//更新終わり
		serializedObject.ApplyModifiedProperties();
	}

}

#endif