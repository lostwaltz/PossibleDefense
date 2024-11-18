using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private Stage stage;
    [SerializeField] private ObjectPool tileObjectPool;
    public Stage Stage { get => stage; set => stage = value; }
    public ObjectPool TileObjectPool { get => tileObjectPool; }

    [SerializeField] private int CallStageNum = 0;
    StringBuilder stringBuilder = new StringBuilder();

    private List<List<StageTileTag>> curMapMatrixData; 
    public StageTileTag[][] curStageMapData; //현재 진행중인 스테이지의 맵 2차월 배열 

    protected override void Awake()
    {
        base.Awake();

        //ObjectPool이 Inspector에서 참조가 안된경우 체크
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
        stringBuilder.Clear();
        stringBuilder.Append(StageConstain.MapMatrixDBPath);
        stringBuilder.Append(CallStageNum.ToString());

        GameObject obj = new GameObject("Stage");
        stage = obj.AddComponent<Stage>();

        curMapMatrixData = CSVReader.LoadMapMatrixFromCSV(stringBuilder.ToString());
        curStageMapData = convertToArray(curMapMatrixData);
        stage.MapInitialize(curMapMatrixData);
    }

    public static StageTileTag[][] convertToArray(List<List<StageTileTag>> list)
    {
        StageTileTag[][] result = new StageTileTag[list.Count][];

        for (int i = 0; i < list.Count; i++)
        {
            result[i] = new StageTileTag[list[i].Count];
        }

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Count; j++)
            {
                result[i][j] = list[i][j];
            }
        }

        return result;
    }

}
