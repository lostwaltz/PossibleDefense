using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    public string sceneName;

    public void Test()
    {
        SceneManager.LoadScene(sceneName);
    }
}
