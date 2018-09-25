using System;
using UnityEngine;

public static class StringUtil
{
	public static string FileName (string path)
	{
		char[] slash = new char[] { '/', '.' };
		var objName = path.Split (slash);
		return objName[objName.Length - 2];
	}
	public static bool TailMatch (string path, string tail)
	{
		return path.Substring (path.Length - tail.Length)== tail;
	}
}