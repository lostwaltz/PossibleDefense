using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneDataBase", menuName = "ScriptableObjects/SceneDataBase")]
public class SceneDataBase : ScriptableObject
{
    public List<SceneData> sceneDataList;
}
