using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private float tileHalfWidth = 5f; // 타일의 절반 가로
    [SerializeField] private float tileHalfHeight = 5f; // 타일의 절반 세로

    public int SpacingRow; // 타일과 타일 사이 여백 y축
    public int SpacingColumn; // 타일과 타일 사이 여백 x축

    public float offsetRow;//열 , 세로
    public float offsetColumn;//행 ,가로

    [SerializeField] private List<List<BaseTile>> stageTiles = new List<List<BaseTile>>();
    [SerializeField] private List<TowerTile> towerTiles = new List<TowerTile>(); // 플레이어가 배치할 수 있는 타일의 월드좌표로 저장한 리스트 
    [SerializeField] private List<SpawnTile> spawnTiles = new List<SpawnTile>(); //Enemy들이 등장할 월드 좌표 데이터 
    [SerializeField] private List<EnemyWayTile> enemyWayTiles = new List<EnemyWayTile>(); // Enemy의 WayPoint를 월드좌표로 저장한 리스트

    public List<TowerTile> TowerTiles { get => towerTiles; }
    public List<SpawnTile> SpawnTiles { get => spawnTiles; }
    public List<EnemyWayTile> EemyWayTiles { get => enemyWayTiles; }
    

    //맵 세팅 : Tile을 월드좌표로 변환하여 월드좌표에 설치하기 기능 
    public void MapInitialize(List<List<StageTileTag>> curMapMatrix)
    {

        for (int z = 0; z < curMapMatrix.Count; z++)
        {
            List<BaseTile> CoulumnTileWorldPos = new List<BaseTile>();

            for (int x = 0; x < curMapMatrix[z].Count; x++)
            {
                BaseTile tile = StageManager.Instance.TileObjectPoolLegacy.SpawnFromPool(curMapMatrix[z][x].ToString()).GetComponent<BaseTile>();
                tile.transform.position = SetTileWorldPos(x, z);

                CoulumnTileWorldPos.Add(tile);

                switch (curMapMatrix[z][x])
                {
                    case StageTileTag.TowerTile:
                        TowerTile towerTile = tile as TowerTile;
                        towerTile.Index = towerTiles.Count;
                        towerTiles.Add(tile as TowerTile);
                        break;

                    case StageTileTag.SpawnTile:
                        spawnTiles.Add(tile as SpawnTile);
                        break;

                    case StageTileTag.EnemyWayTile:
                        enemyWayTiles.Add(tile as EnemyWayTile);
                        break;
                }
            }

            stageTiles.Add(CoulumnTileWorldPos);
        }

    }

    //Tile의 position값을 배열좌표 -> 월드좌표롤 변경하는 코드
    public Vector3 SetTileWorldPos(int x, int z)
    {
        Vector3 worldPos = new Vector3((x * tileHalfWidth) + SpacingColumn + offsetColumn, 0, (-z * tileHalfHeight) + SpacingRow + offsetRow);
        return worldPos;
    }
}
