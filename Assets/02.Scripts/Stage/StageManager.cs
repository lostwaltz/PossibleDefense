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
    private List<Vector3> curStageEnmeyWayPointData;

    public StageTileTag[][] curStageMapData; //현재 진행중인 스테이지의 맵 2차월 배열 
    public Vector3[] curEnmeyWayPointData; //현재 진행중인 스테이지의 웨이포인트 배열

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


        GameObject obj = new GameObject("Stage");
        Stage Collstage = obj.AddComponent<Stage>();
        stage = Collstage;

        //
        stringBuilder.Clear();
        stringBuilder.Append(StageConstain.MapMatrixDBPath);
        stringBuilder.Append(CallStageNum.ToString());

        curMapMatrixData = CSVReader.LoadMapMatrixFromCSV(stringBuilder.ToString());
        curStageMapData = convertToArray(curMapMatrixData);

        stage.MapInitialize(curMapMatrixData);

        //

        stringBuilder.Clear();
        stringBuilder.Append(StageConstain.StageWayPointDBPath);
        stringBuilder.Append(CallStageNum.ToString());

        curStageEnmeyWayPointData = CSVReader.LoadStageWayPointFromCSV(stringBuilder.ToString());

        stage.EnemyWayPointInitialize(curStageEnmeyWayPointData);
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
