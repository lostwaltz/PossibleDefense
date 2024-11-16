using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private Enemy[] Enemies; //�ش� ���������� ������ �� ĳ���� , ���߿� ������Ʈ Ǯ������ �����ؾ��� 
    private int EnemyIndex; //���� ���� �ε���

    public int SpacingRow; // Ÿ�ϰ� Ÿ�� ���� ���� y��
    public int SpacingColumn; // Ÿ�ϰ� Ÿ�� ���� ���� x��

    public float offsetRow;//�� , ����
    public float offsetColumn;//�� ,����

    public Vector3 StartPosition;

    private List<Vector3> WayPointWorldPos; // ���߿��� Queue�� �ۼ��Ͽ� �������� �Ҽ� �ְ� ����Ұ�
    private List<Vector3> PlayerTilePointWorldPos; // �÷��̾ ��ġ�� �� �ִ� Ÿ�� 

    public MapDataSO curMapData;//���� ���ӿ��� ����� �� �����͸� �����ϰ� �ִ� Class ������ 

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

        //Enemy WayPoint �ʱ�ȭ
        for (int i = 0; i < WayPointWorldPos.Count; i++)
        {
            WayPointWorldPos[i] = SetTileWorldPos((int)curMapData.WayPoint[i].x, (int)curMapData.WayPoint[i].z);
        }

        //Player TilePoint �ʱ�ȭ
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
