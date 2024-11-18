using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Enemy[] Enemies; //해당 스테이지에 등장할 적 캐릭터 , 나중에 오브젝트 풀링으로 구현해야함 
    private int EnemyIndex; //등장 몬스터 인덱스

    public int SpacingRow; // 타일과 타일 사이 여백 y축
    public int SpacingColumn; // 타일과 타일 사이 여백 x축

    public float offsetRow;//열 , 세로
    public float offsetColumn;//행 ,가로

    [SerializeField] private Vector3 StartPosition;

    [SerializeField] private List<List<Vector3>> StageTileWorldPos = new List<List<Vector3>>();

    [SerializeField] private List<Vector3> WayPointWorldPos = new List<Vector3>(); // 나중에는 Queue로 작성하여 동적생성 할수 있게 사용할거
    [SerializeField] private List<Vector3> PlayerTilePointWorldPos = new List<Vector3>(); // 플레이어가 배치할 수 있는 타일 

    public void MapInitialize(List<List<StageTileTag>> curMapMatrix)
    {
        GameObject tile = null;
        for (int z = 0; z < curMapMatrix.Count; z++)
        {
            List<Vector3> CoulumnTileWorldPos = new List<Vector3>();

            for (int x = 0; x < curMapMatrix[z].Count; x++)
            {
                tile = StageManager.Instance.TileObjectPool.SpawnFromPool(curMapMatrix[z][x].ToString());

                CoulumnTileWorldPos.Add(SetTileWorldPos(tile, x, z));

                switch (curMapMatrix[z][x])
                {
                    case StageTileTag.PlayerTile:
                        PlayerTilePointWorldPos.Add(tile.transform.position);
                        break;
                    case StageTileTag.EnemyMoveStartTile:
                        StartPosition = tile.transform.position;
                        break;
                    case StageTileTag.EnemyMoveEndTile:
                        break;
                }
            }

            StageTileWorldPos.Add(CoulumnTileWorldPos);
        }

    }

    public void EnemyWayPointInitialize(List<Vector3> curEnemyWayPoint)
    {
        //Enemy WayPoint 초기화
        for (int i = 0; i < curEnemyWayPoint.Count; i++)
        {
            WayPointWorldPos[i] = SetTileWorldPos((int)curEnemyWayPoint[i].x, (int)curEnemyWayPoint[i].z);
        }

        //Debug
        foreach(Vector3 pos in WayPointWorldPos)
        {
            Debug.Log($"EnemyWayPoint : {pos}");
        }
        //
    }

    private Vector3 SetTileWorldPos(GameObject tile, int x, int z)
    {
        Vector3 worldPos = new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-z * 5) + SpacingRow + offsetRow);
        tile.transform.position = worldPos;

        return worldPos;
    }
    private Vector3 SetTileWorldPos(int x, int z)
    {
        Vector3 worldPos = new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-z * 5) + SpacingRow + offsetRow);
        return worldPos;
    }
}
