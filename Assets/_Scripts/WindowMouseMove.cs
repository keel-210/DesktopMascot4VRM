/**
 * WindowController sample
 * 
 * Author: Kirurobo, http://twitter.com/kirurobo
 * Copyright: (c) 2014 Kirurobo
 * License: Unlicense
 * 
 * This is free and unencumbered software released into the public domain.
 * 
 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.
 * 
 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 * 
 * For more information, please refer to <http://unlicense.org/>
 */
using System.Collections;
using UnityEngine;

public class WindowMouseMove : MonoBehaviour
{
	WindowController Window;
	string Title = "";
	void Awake ()
	{
		// ウィンドウ制御用のインスタンス作成
		Window = new WindowController ();
		Title = WindowController.GetProjectName ();
		FindMyWindow ();
	}
	void Update ()
	{
		if (Input.GetAxis ("Fire1")> 0)
		{
			float sensitivity = 0.1f;
			float mouse_move_x = Input.GetAxis ("Mouse X")* sensitivity;
			float mouse_move_y = Input.GetAxis ("Mouse Y")* sensitivity;
			Vector2 axes = new Vector2 (mouse_move_x, -mouse_move_y);
			Vector2 windowPosition = Window.GetPosition (); // 現在のウィンドウ位置を取得
			windowPosition += axes * 10f; // ウィンドウ位置に上下左右移動分を加える。係数10.0fは適当。
			Window.SetPosition (windowPosition); // ウィンドウ位置を設定
		}
	}
	private void FindMyWindow ()
	{
		// まず自分のウィンドウタイトルで探す
		if (Title != "" || !Window.FindHandleByTitle (Title))
		{
			// 名前がダメならアクティブなウィンドウを取得
			Window.FindHandle ();
		}
	}
}