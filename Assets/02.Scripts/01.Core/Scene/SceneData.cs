using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    public SceneAsset sceneAsset;

    public void Test()
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}
