using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SceneDataBase", menuName = "ScriptableObjects/SceneDataBase")]
public class SceneDataBase : ScriptableObject
{
    public List<SceneData> sceneDataList;
}


[System.Serializable]
public class SceneData
{
    public SceneAsset sceneAsset;

    public void Test()
    {
        SceneManager.LoadScene(sceneAsset.name);
    }
}
