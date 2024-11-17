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

    public Vector3 StartPosition;

    private List<Vector3> WayPointWorldPos; // 나중에는 Queue로 작성하여 동적생성 할수 있게 사용할거
    private List<Vector3> PlayerTilePointWorldPos; // 플레이어가 배치할 수 있는 타일 

    public MapDataSO curMapData;//현재 게임에서 사용할 맵 데이터를 저장하고 있는 Class 데이터 

    public void MapInitialize()
    {
        WayPointWorldPos = curMapData.WayPoint;
        PlayerTilePointWorldPos = curMapData.PlayerTilePoint;

        GameObject tile = new GameObject();
        for (int z = 0; z < curMapData.Row; z++)
        {
            for (int x = 0; x < curMapData.Column; x++)
            {
                switch ((StageTileTag)curMapData.MapMatrix[z].index[x])
                {
                    case StageTileTag.EnemyTile:
                        tile = StageManager.Instance.TileObjectPool.SpawnFromPool(StageTileTag.EnemyTile.ToString());
                        break;

                    case StageTileTag.PlayerTile:
                        tile = StageManager.Instance.TileObjectPool.SpawnFromPool(StageTileTag.PlayerTile.ToString());
                        break;

                    case StageTileTag.EnemyMoveStartTile:
                        tile = StageManager.Instance.TileObjectPool.SpawnFromPool(StageTileTag.EnemyMoveStartTile.ToString());
                        break;

                    case StageTileTag.EnemyMoveEndTile:
                        tile = StageManager.Instance.TileObjectPool.SpawnFromPool(StageTileTag.EnemyMoveEndTile.ToString());
                        break;

                }

                SetTileWorldPos(tile, x, z);
            }
        }

        //Enemy WayPoint 초기화
        for (int i = 0; i < WayPointWorldPos.Count; i++)
        {
            WayPointWorldPos[i] = SetTileWorldPos((int)curMapData.WayPoint[i].x, (int)curMapData.WayPoint[i].z);
        }

        //Player TilePoint 초기화
        for (int i = 0; i < curMapData.PlayerTilePoint.Count; i++)
        {
            PlayerTilePointWorldPos[i] = SetTileWorldPos((int)curMapData.PlayerTilePoint[i].x, (int)curMapData.PlayerTilePoint[i].z);
        }
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
