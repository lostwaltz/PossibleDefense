using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private Stage stage; //현재 사용될 Stage 변수
    [SerializeField] private ObjectPool tileObjectPool; //타일을 꺼내는 오브젝트 Pool

    [SerializeField] private int callStageNum = 0; //호출할 스테이지 넘버링 

    private int curEnemyCount = 0; // 현재 필드에 있는 적의 갯수
    private int finishEnemyCount = 100; // 필드에 해당 Enemy 갯수 이상되면 게임오버되는 갯수

    private Enemy[] Enemies; //해당 스테이지에 등장할 적 캐릭터 , 나중에 오브젝트 풀링으로 구현해야함 
    private int EnemyIndex; //등장 몬스터 인덱스

    private float waveTimer;
    private WaveStageData curWave; //현재 Wave Data
    private Coroutine waveCoroutine;

    private List<List<StageTileTag>> curMapMatrixData; //현재 스테이지 타일 데이터를 저장한 변수 (맵 데이터,월드좌표x,배열좌표o)
    private List<Vector3> curStageEnmeyWayPointData; //현재 스테이지의 Enemy의 WayPoint 데이터를 저장한 변수 (월드좌표x,배열좌표o)
    private Queue<WaveStageData> curWaveStageData; //현재 스테이지의 Wave데이터


    public StageTileTag[][] curStageMapData; //현재 진행중인 스테이지의 맵 2차월 배열 
    public Vector3[] curEnmeyWayPointData; //현재 진행중인 스테이지의 웨이포인트 배열

    public Stage Stage { get => stage; }
    public ObjectPool TileObjectPool { get => tileObjectPool; }

    StringBuilder stringBuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 멤버변수로 선언

    //debug
    public TestEnemyMovement TestEnemyPrefeb;
    //

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
        //Debug
        GameStartInit(callStageNum);
    }

    //외부 Scene에서 게임 씬 동작하는 코드입니다. 
    public void GameStartInit(int callStageNum)
    {
        this.callStageNum = callStageNum;

        MapSetting();

        WaveSetting();
    }

    //게임 시작전 맵 세팅하는 코드 
    private void MapSetting()
    {
        //월드에서 사용할 Stage Object 생성 
        GameObject obj = new GameObject("Stage");
        Stage Collstage = obj.AddComponent<Stage>();
        stage = Collstage;

        CreateMapMatrix();
        CreateWayPoint();
        CrateWaveData();
    }

    private void WaveSetting()
    {
        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
            waveCoroutine = null;
        }

        curWave = curWaveStageData.Dequeue();
        waveTimer = curWave.WaveTime;

        waveCoroutine = StartCoroutine(OperateWave(curWave));
    }

    private void CrateWaveData()
    {
        StringBuilderSet(StageConstain.StageWaveStageDBPath, callStageNum);

        curWaveStageData = CSVReader.LoadWaveStageFromCSV(stringBuilder.ToString());
    }

    //월드 좌표에 외부데이터(CSV)를 토대로 맵을 생성하는 코드
    private void CreateMapMatrix()
    {
        StringBuilderSet(StageConstain.MapMatrixDBPath, callStageNum);

        curMapMatrixData = CSVReader.LoadMapMatrixFromCSV(stringBuilder.ToString());
        curStageMapData = convertToArray(curMapMatrixData);

        stage.MapInitialize(curMapMatrixData);
    }

    //Enemy가 이동할 WayPoint를 외부데이터(CSV)를 토대로 생성하는 코드
    private void CreateWayPoint()
    {
        StringBuilderSet(StageConstain.StageWayPointDBPath, callStageNum);

        curStageEnmeyWayPointData = CSVReader.LoadStageWayPointFromCSV(stringBuilder.ToString());

        EnemyWayPointToWorldPos(curStageEnmeyWayPointData);
    }

    //Enemy의 WayPoint를 배열좌표 -> 월드좌표롤 변경하는 코드
    public void EnemyWayPointToWorldPos(List<Vector3> curEnemyWayPoint)
    {
        curEnmeyWayPointData = new Vector3[curEnemyWayPoint.Count];

        for (int i = 0; i < curEnemyWayPoint.Count; i++)
        {
            Vector3 enemyWayTilePos = stage.SetTileWorldPos((int)curEnemyWayPoint[i].x, (int)curEnemyWayPoint[i].z);
            curEnmeyWayPointData[i] = (enemyWayTilePos);
        }
    }

    //스트링 빌더를 이용한 스테이지 번호 세팅
    private void StringBuilderSet(string assetPath, int stageNum)
    {
        stringBuilder.Clear();
        stringBuilder.Append(assetPath);
        stringBuilder.Append(stageNum.ToString());
    }

    ////Enemy의 WayPoint 월드좌표 리스트 반환(외부클래스에서 반환)
    //public Vector3[] GetCurEnemyWayPointWorldPos()
    //{
    //    Vector3[] EnemyWayPointWorldPos;
    //    EnemyWayPointWorldPos = stage.WayPointWorldPos.ToArray();
    //    return EnemyWayPointWorldPos;
    //}

    ////SlimeTower 배치가 가능한 타일 배열 반환하기 
    //public Vector3[] GetCuPlayerTowerWorldPos()
    //{
    //    Vector3[] PlayerTowerWorldPos;
    //    PlayerTowerWorldPos = stage.WayPointWorldPos.ToArray();
    //    return PlayerTowerWorldPos;
    //}

    //2차원 List를 2차원 배열로 변경하는 코드 (외부에서 사용 할수 있게 만든 코드)
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

    private void Update()
    {
        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0)
        {
            WaveSetting();
        }
    }

    //Debug : UI 매니저가 없기에 현재 Inspector로 연결 해놓은상태 
    public UI_WaveIndicator uI_WaveIndicator;
    public UI_EnemyCount uI_EnemyCount;
    private void LateUpdate()
    {
        //UIManager.Instance.UIContainer[UI_WaveIndicator].UI_Print;
        uI_WaveIndicator.UIPrint(waveTimer, curWave.WaveNum, curEnemyCount);
        uI_EnemyCount.UIPrint(finishEnemyCount, curEnemyCount);
    }
    IEnumerator OperateWave(WaveStageData waveData)
    {
        float spawnTime = waveData.WaveSpawnData[EnemyType.Rabit].EnemySpawnTimer;
        WaitForSeconds SpawnTimer = new WaitForSeconds(spawnTime);
        int count = 0;
        
        while (count < waveData.WaveSpawnData[EnemyType.Rabit].EnemyCount)
        {
            //ToDoCode : EnemySpawnManager를 통해서 Enemy를 오브젝트 풀링하고 Way를 설정해주는 코드 입력
            count++;
            curEnemyCount++;

            ////Debug : WayPoint Test Code
            TestEnemyMovement newEnemy = Instantiate(TestEnemyPrefeb);
            newEnemy.transform.position = stage.SpawnTiles[0].transform.position;
            newEnemy.Initialize(curEnmeyWayPointData);
            newEnemy.OperateEnemy();

            yield return SpawnTimer;
        }
    }


    //게임 오버 조건
    private bool GameOverCheck()
    {
        //필드에 적갯수가 초과 됬을때
        if(finishEnemyCount <= curEnemyCount)
        {
            Debug.Log("게임 오버");
            this.enabled = false;
            return true;
        }
        //해당 Wave의 Boss가 시간내에 잡히지 않았을때

        return false;
    }

    //게임 클리어 조건
    private bool GameClearCheck()
    {
        if (finishEnemyCount <= curEnemyCount)
        {
            Debug.Log("게임 클리어");
            this.enabled = false;
            return true;
        }

        return false;
    }
    //스테이지의 유닛 강화 조건
}
