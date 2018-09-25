using System.Collections;
using UnityEngine;

public class DirectoryPathAttribute : PropertyAttribute
{
	public string extensionFilter;
	public DirectoryPathAttribute (string extensionFilter = "")
	{
		if (string.IsNullOrEmpty (extensionFilter))
		{
			extensionFilter = "";
		}
		this.extensionFilter = extensionFilter;
	}
}