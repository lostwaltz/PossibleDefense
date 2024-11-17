using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private Stage stage;
    [SerializeField] private ObjectPool tileObjectPool;
    public Stage Stage { get => stage; set => stage = value; }
    public ObjectPool TileObjectPool { get => tileObjectPool; }

    [SerializeField] private MapDataSO[] MapData;

    [SerializeField] private int CallStageNum = 0;

    protected override void Awake()
    {
        base.Awake();

        //ObjectPool�� Inspector���� ������ �ȵȰ�� üũ
        if (tileObjectPool == null)
        {
            tileObjectPool = GetComponent<ObjectPool>();
        }
    }

    private void Start()
    {
        MapSetting();
    }

    public void MapSetting()
    {
        GameObject obj = new GameObject("Stage");
        stage = obj.AddComponent<Stage>();
        stage.curMapData = MapData[CallStageNum-1]; //Stage1���� ������ ����
        stage.MapInitialize();
    }
    
}
