using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    //0 : EnemyTile  , 1 : Player Tile  , 2 : EnemyMoveStart , 3: EnemyMoveFinisi , 4: EnemyMoveStartFinish
    public int[,] boardMatrix =
    {
        {0,0,0,0,0,2},
        {0,1,1,1,1,0},
        {0,1,1,1,1,0},
        {0,1,1,1,1,0},
        {0,1,1,1,1,0},
        {0,0,0,0,0,0},
    };

    public List<Vector3> WayPoint; // 나중에는 Queue로 작성하여 동적생성 할수 있게 사용할거
    public List<Vector3> PlayerTilePoint; // 플레이어가 배치할 수 있는 타일 
}

public class Board : MonoBehaviour
{
    public Enemy[] Enemies; //해당 스테이지에 등장할 적 캐릭터 , 나중에 오브젝트 풀링으로 구현해야함 
    public int EnemyIndex; //등장 몬스터 인덱스

    public GameObject EnemyTile;
    public GameObject PlayerTile;
    public GameObject EnemyMoveStartTile;
    public GameObject EnemyMoveEndTile;

    public int Row;//열 , 세로
    public int Column;//행 ,가로

    public int SpacingRow; // 타일과 타일 사이 여백 y축
    public int SpacingColumn; // 타일과 타일 사이 여백 x축

    public float offsetRow;//열 , 세로
    public float offsetColumn;//행 ,가로

    public Vector3 StartPosition;
    public Vector3 EndPosition;

    public MapData curMap;//현재 게임에서 사용할 맵 데이터를 저장하고 있는 Class 데이터 
    

    private void Awake()
    {
        GameObject Tile = new GameObject();
        for (int y = 0; y < Row; y++)
        {
            for (int x = 0; x < Column; x++)
            {
                switch (curMap.boardMatrix[y, x])
                {
                    case 0:
                        Tile = Instantiate(EnemyTile, transform);
                        break;
                    case 1:
                        Tile = Instantiate(PlayerTile, transform);
                        curMap.PlayerTilePoint.Add(new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-y * 5) + SpacingRow + offsetRow));
                        break;
                    case 2:
                        Tile = Instantiate(EnemyMoveStartTile, transform);
                        StartPosition = new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-y * 5) + SpacingRow + offsetRow);
                        break;
                    case 3:
                        Tile = Instantiate(EnemyMoveEndTile, transform);
                        EndPosition = new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-y * 5) + SpacingRow + offsetRow);
                        break;
                }

                Tile.transform.position = new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-y * 5) + SpacingRow + offsetRow);
            }
        }
    }

    private void Start()
    {
        Vector3 pos;
        for (int i = 0; i < curMap.WayPoint.Count; i++)
        {
            float x = curMap.WayPoint[i].x * 5 + SpacingColumn + offsetColumn;
            float z = (-curMap.WayPoint[i].z * 5) + SpacingRow + offsetRow;
            curMap.WayPoint[i] = new Vector3(x, 0, z);
        }


        InvokeRepeating("SummonEnemy", 0.5f, 2.0f);
    }


    private void SummonEnemy()
    {
        if (EnemyIndex < Enemies.Length)
        {
            Enemy poolObject = Instantiate(Enemies[EnemyIndex]);
            poolObject.transform.position = StartPosition;
            poolObject.WayPoint = curMap.WayPoint;
            EnemyIndex++;
        }
    }
}
