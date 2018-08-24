using UnityEngine;
using UnityEngine.SceneManagement;

public class DMMSceneChange : MonoBehaviour
{
    public string nextSceneName;

    void Start() { }

    public void SceneChange()
    {
        SceneManager.LoadScene(nextSceneName); //Application.LoadLevel(nextSceneName);
    }
}
