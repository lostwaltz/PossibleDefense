using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    public MobSpawner MobSpawner;
    
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("StageManager");
                instance = singletonObject.AddComponent<StageManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

}