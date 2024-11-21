using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingletonDontDestroy<SceneLoadManager>
{
    private SceneDataBase _dataBase;

    public static string CurrentScene { get; private set; }
    public static string PrevScene { get; private set; }
    public static string NextScene { get; private set; }

    private readonly Dictionary<string, SceneBase> _sceneDic = new();

    private Action<float> _onLoadingProgressUpdate;

    protected override void Awake()
    {
        base.Awake();
        
        _dataBase = Resources.Load<SceneDataBase>("DataSheet/SceneData".ToString());

        foreach (var data in _dataBase.sceneDataList)
        {
            Type type = Type.GetType(data.sceneAsset.name);

            if (type == null || !typeof(SceneBase).IsAssignableFrom(type))
            {
                Debug.LogError($"Can't find scene type {data.sceneAsset.name}");
                continue;
            }

            MethodInfo method = typeof(SceneFactory).GetMethod("CreateFactory")?.MakeGenericMethod(type);

            if (method == null) return;

            _sceneDic.Add(data.sceneAsset.name, (SceneBase)method.Invoke(null, null));
        }
        
        CurrentScene = SceneManager.GetActiveScene().name;
        _sceneDic[CurrentScene].OnEnter();
    }

    public void LoadScene(string sceneName)
    {
        NextScene = sceneName;

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(NextScene.ToString());
        if (op != null) op.allowSceneActivation = false;

        while (op is { isDone: false })
        {
            yield return null;

            _onLoadingProgressUpdate?.Invoke(op.progress);

            if (op.progress < 0.9f)
                continue;

            op.allowSceneActivation = true;

            break;
        }

        PrevScene = CurrentScene;
        CurrentScene = NextScene;

        _sceneDic[PrevScene].OnExit();
        _sceneDic[CurrentScene].OnEnter();
    }
}
