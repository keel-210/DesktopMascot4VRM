//  PerlinNoiseAnimeRandomizer.cs
//  http://kan-kikuchi.hatenablog.com/entry/PerlinNoise_Anime
//
//  Created by kan.kikuchi on 2018.12.01.

using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// パーリンノイズを使ってアニメーションにランダムなゆらぎを与えるクラス
/// </summary>
public class PerlinNoiseAnimeRandomizer : MonoBehaviour
{

	//パーリンノイズのシード(厳密にはシードではないがシード代わりに使っている値)
	private Vector3 _seed = Vector3.zero;

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
		//同じアニメにならないようにランダムにシードを決定
		_seed = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
	}

	/// <summary>
	/// 状態(ゆらぎの倍率、速さ)を設定する
	/// </summary>
	public void SetStatus(float wholeScale, Vector3 xyzScale, float frequencyRate)
	{
		_wholeScale = wholeScale;
		_xyzScale = xyzScale;
		_frequencyRate = frequencyRate;
	}

	//=================================================================================
	//更新
	//=================================================================================

	private void LateUpdate()
	{

		//振れ幅が大きく、振動数が少ない(変動が遅い)、大きいノイズを作成
		Vector3 bigNoize = CreateVector3Noise(ratio: 0.8f, frequencyRate: _frequencyRate * 0.4f);

		//振れ幅が小さく、振動数が多い(変動が早い)、小さいノイズを作成
		Vector3 smallNoize = CreateVector3Noise(ratio: 0.1f, frequencyRate: _frequencyRate);

		//大小のノイズを合成し、アニメで設定された現在のRotationを元にゆらぎを与える
		transform.localRotation = transform.localRotation * Quaternion.Euler((bigNoize + smallNoize) * _wholeScale);

	}

	//=================================================================================
	//作成
	//=================================================================================

	//Vector3のノイズをパーリンノイズで作成(ratioが振れ幅の倍率、frequencyRateが変動の速さの倍率)
	private Vector3 CreateVector3Noise(float ratio, float frequencyRate)
	{
		float frequency = Time.time * frequencyRate;

		//Mathf.PerlinNoiseは0 ~ 1の値を返すので0.5を引いて-0.5 ~ 0.5に補正
		return new Vector3(
			(Mathf.PerlinNoise(frequency, _seed.x) - 0.5f) * ratio * _xyzScale.x,
			(Mathf.PerlinNoise(frequency, _seed.y) - 0.5f) * ratio * _xyzScale.y,
			(Mathf.PerlinNoise(frequency, _seed.z) - 0.5f) * ratio * _xyzScale.z
		);
	}

}