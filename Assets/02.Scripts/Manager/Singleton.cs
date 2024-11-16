using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static bool HasInstance => _instance != null;

    private float InitializationTime { get; set; }

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindAnyObjectByType<T>();
            
            if (_instance != null) return _instance;

            var go = new GameObject
            {
                name = typeof(T).Name + " Auto-Generated",
                hideFlags = HideFlags.HideAndDontSave
            };

            _instance = go.AddComponent<T>();
            
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying) return;

        InitializationTime = Time.time;

        var oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);

        foreach (var old in oldInstances)
        {
            if (old.GetComponent<Singleton<T>>().InitializationTime < InitializationTime)
                Destroy(old.gameObject);
        }

        if (_instance == null)
            _instance = this as T;
    }
}
