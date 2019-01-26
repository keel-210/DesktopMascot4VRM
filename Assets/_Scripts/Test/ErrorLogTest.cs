using UnityEngine;

public class ErrorLogTest : MonoBehaviour
{
	void Start()
	{
		FindObjectOfType<ErrorReciever>().Error("Error 000 : Test Error");
	}
}