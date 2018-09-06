using System.Collections;
using UnityEngine;

public class FilePathAttribute : PropertyAttribute
{
	/// <summary>
	/// ダイアログの拡張子フィルタ
	/// </summary>
	public string extensionFilter;

	/// <summary>
	/// コンストラクタ
	/// 
	/// フィルタの指定が無かった場合は空文字列にする
	/// </summary>
	public FilePathAttribute (string extensionFilter = "")
	{
		if (string.IsNullOrEmpty (extensionFilter))
		{
			extensionFilter = "";
		}
		this.extensionFilter = extensionFilter;
	}
}