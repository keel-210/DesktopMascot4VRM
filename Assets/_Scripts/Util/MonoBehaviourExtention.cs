using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///

/// MonoBehaviorの拡張クラス
///
public static class MonoBehaviorExtentsion
{
	/// <summary>
	/// 渡されたメソッドを指定時間後に実行する
	/// </summary>
	public static IEnumerator DelayMethod<T1, T2> (this MonoBehaviour mono, float waitTime, Action<T1, T2> action, T1 t1, T2 t2)
	{
		yield return new WaitForSeconds (waitTime);
		action (t1, t2);
	}

	/// <summary>
	/// 渡されたメソッドを指定時間後に実行する
	/// </summary>
	public static IEnumerator DelayMethod<T> (this MonoBehaviour mono, float waitTime, Action<T> action, T t)
	{
		yield return new WaitForSeconds (waitTime);
		action (t);
	}

	/// <summary>
	/// 渡されたメソッドを指定時間後に実行する
	/// </summary>
	public static IEnumerator DelayMethod (this MonoBehaviour mono, float waitTime, Action action)
	{
		yield return new WaitForSeconds (waitTime);
		action ();
	}
}