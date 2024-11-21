using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Assertions.Must;
using System.Linq;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private Stage stage; //현재 사용될 Stage 변수
    [FormerlySerializedAs("tileObjectPool")] [SerializeField] private ObjectPoolLegacy tileObjectPoolLegacy; //타일을 꺼내는 오브젝트 Pool

    [SerializeField] private int callStageNum = 0; //호출할 스테이지 넘버링 
    [SerializeField] private UpgradesController upgradeController;
    [SerializeField] private WaveSkip waveSkip;
    [SerializeField] private TowerSell towerSell;

    StringBuilder stringBuilder = new StringBuilder(); //문자열 최적화를 위한 스트링빌더 멤버변수로 선언

    private int curEnemyCount = 0; // 현재 필드에 있는 적의 갯수
    private int finishEnemyCount = 100; // 필드에 해당 Enemy 갯수 이상되면 게임오버되는 갯수

    private float waveTimer;
    private WaveStageData curWave; //현재 Wave Data
    private bool allWaveFinish = false; //모든 웨이브가 소환 된 경우 체크하는 변수

    private int curGold = 100000;
    private int summonTowerCost = 100;

    private int curTowerCount = 0; //현재 설치된 타워의 갯수 
    private List<List<StageTileTag>> curMapMatrixData; //현재 스테이지 타일 데이터를 저장한 변수 (맵 데이터,월드좌표x,배열좌표o)
    private List<Vector3> curStageEnmeyWayPointData; //현재 스테이지의 Enemy의 WayPoint 데이터를 저장한 변수 (월드좌표x,배열좌표o)
    private Queue<WaveStageData> curWaveStageData; //현재 스테이지의 Wave데이터
    private int maxWaveCount;

    public StageTileTag[][] curStageMapData; //현재 진행중인 스테이지의 맵 2차월 배열 
    [HideInInspector] public Vector3[] curEnmeyWayPointData; //현재 진행중인 스테이지의 웨이포인트 배열

    public Stage Stage { get => stage; }
    public ObjectPoolLegacy TileObjectPoolLegacy { get => tileObjectPoolLegacy; }
    public int CurGold { get => curGold; set => curGold = value; }
    public int SummonTowerCost { get => summonTowerCost; }
    public int CurEnemyCount { get => curEnemyCount; set => curEnemyCount = value; }
    public Queue<WaveStageData> CurWaveStageData { get => curWaveStageData; }

    //타워 판매 필드
    public bool IsSellMode; //타워를 판매하는 모드에 진입체크 변수 
    public TowerSell TowerSell { get => towerSell; }
    public int CurTowerCount { get => curTowerCount; set => curTowerCount = value; }

    [SerializeField] private Button SellModeButton;

    //타워 생성 필드
    [SerializeField] private SlimeTowerSpawner slimeTowerSpawner;
    [SerializeField] private Button spawnButton;

    //Debug : UI 매니저가 없기에 현재 Inspector로 연결 해놓은상태 
    public UI_WaveIndicator uI_WaveIndicator;
    public UI_EnemyCount uI_EnemyCount;
    public UI_CurGoldIndicator uI_CurGoldIndicator;
    public UIReslut UIReslut;

    protected override void Awake()
    {
        base.Awake();

        callStageNum = GameManager.Instance.CallStageNum;

        //ObjectPool이 Inspector에서 참조가 안된경우 체크
        if (tileObjectPoolLegacy == null)
        {
            tileObjectPoolLegacy = GetComponent<ObjectPoolLegacy>();
        }

        if(upgradeController == null)
        {
            upgradeController = GetComponent<UpgradesController>();
        }

        if(waveSkip == null)
        {
            waveSkip = GetComponent<WaveSkip>();     
        }

        if(slimeTowerSpawner == null)
        {
            slimeTowerSpawner = GetComponent<SlimeTowerSpawner>();
        }
        if(towerSell == null)
        {
            towerSell = GetComponent<TowerSell>();
        }

      
        //Debug
    
        //SellModeButton.onClick.AddListener(() => IsSellMode = !IsSellMode);
    }

    private void Start()
    {
        spawnButton.onClick.AddListener(() => SpawnSlimeTower());
        GameStartInit(callStageNum);
        
    }
 
    public bool UseGold(int useGoldAmount)
    {
        if (curGold >= useGoldAmount)
        {
            curGold = Mathf.Max(0, curGold - useGoldAmount);
            return true;
        }
        return false;
    }

    private void SpawnSlimeTower()
    {
        if (curTowerCount < stage.TowerTiles.Count && UseGold(summonTowerCost))
        {
            GameObject obj = slimeTowerSpawner.SpawnTowerByProbability();
            curTowerCount++;

            for (int i = 0; i < stage.TowerTiles.Count; i++)
            {
                if (stage.TowerTiles[i].SlimeTower == null)
                {
                    stage.TowerTiles[i].SlimeTower = obj;
                    obj.transform.position = stage.TowerTiles[i].transform.position + (Vector3.up * 1.6f);
                    obj.GetComponent<BaseSlimeTower>().CurTowerTileIndex = stage.TowerTiles[i].Index;
                    break;
                }
            }
        }
    }

    public void TowerTileSwap(TowerTile oldTile,TowerTile newTile)
    {
        GameObject tmp = newTile.SlimeTower;
        newTile.SlimeTower = oldTile.SlimeTower;
        oldTile.SlimeTower = tmp;   
    }

    //외부 Scene에서 게임 씬 동작하는 코드입니다. 
    public void GameStartInit(int callStageNum)
    {
        this.callStageNum = callStageNum;

        upgradeController.InitUpgradeData();
        MapSetting();
        WaveSetting();
        TowerController.Instance.Init();
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

    public void WaveSetting()
    {
        if (curWaveStageData.Count != 0)
        {
            curWave = curWaveStageData.Dequeue();
            waveTimer = curWave.WaveTime;

            ICollection<int> keys = curWave.WaveSpawnData.Keys;

            foreach (int id in keys)
            {
                SpawnEnemy(id);
            }
        }
        else
        {
            Debug.Log("모든 웨이브 완료");
            allWaveFinish = true;
        }
    }

    private void CrateWaveData()
    {
        StringBuilderSet(StageConstain.StageWaveStageDBPath, callStageNum);

        curWaveStageData = CSVReader.LoadWaveStageFromCSV(stringBuilder.ToString());
        maxWaveCount = curWaveStageData.Count;
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
        if (!allWaveFinish)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                WaveSetting();
            }
        }
    }

    private void LateUpdate()
    {
        uI_WaveIndicator.UIPrint(waveTimer, curWave.WaveNum, curEnemyCount);
        uI_EnemyCount.UIPrint(finishEnemyCount, curEnemyCount);
        uI_CurGoldIndicator.UIPrint(curGold);

        if (GameOverCheck() || GameClearCheck())
        {
            //TODO : 결과창 팝업
            //
            UIReslut.gameObject.SetActive(true);

        }
    }



    private void SpawnEnemy(int id)
    {
        Vector3 spawnWorldPos = stage.SpawnTiles[0].transform.position;
        float SpawnDelay = curWave.WaveSpawnData[id].EnemySpawnTimer;
        Vector3[] waypoints = curEnmeyWayPointData;
        int spawnCount = curWave.WaveSpawnData[id].EnemyCount;

        SpawnManager.Instance.SetSpawner(spawnWorldPos, SpawnDelay, waypoints, id, spawnCount);
    }

    //게임 오버 조건
    private bool GameOverCheck()
    {
        //필드에 적갯수가 초과 됬을때
        if(finishEnemyCount <= curEnemyCount)
        {
            UIReslut.UpdateUI(false, maxWaveCount - curWaveStageData.Count, callStageNum * (maxWaveCount - curWaveStageData.Count));
            GameManager.Instance.PlayerGold += callStageNum * curStageMapData.Length;

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
        if (curEnemyCount <= 0 && curWaveStageData.Count == 0)
        {
            UIReslut.UpdateUI(true, maxWaveCount, callStageNum * maxWaveCount);
            GameManager.Instance.curClearStageNum = callStageNum;
            GameManager.Instance.PlayerGold += callStageNum * curStageMapData.Length;

            this.enabled = false;
            return true;
        }

        return false;
    }
    //스테이지의 유닛 강화 조건
}
