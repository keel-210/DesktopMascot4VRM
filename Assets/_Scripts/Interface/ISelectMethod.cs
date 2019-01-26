using UnityEngine;
public interface ISelectMethod
{
	GameObject TargetObject { get; set; }
	GameObject GimmickTarget { get; set; }
	string CallbackName { get; set; }
}