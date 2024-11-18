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

    [SerializeField] private Vector3 StartPosition;

    [SerializeField] private List<List<Vector3>> StageTileWorldPos = new List<List<Vector3>>();

    [SerializeField] private List<Vector3> WayPointWorldPos = new List<Vector3>(); // ���߿��� Queue�� �ۼ��Ͽ� �������� �Ҽ� �ְ� ����Ұ�
    [SerializeField] private List<Vector3> PlayerTilePointWorldPos = new List<Vector3>(); // �÷��̾ ��ġ�� �� �ִ� Ÿ�� 

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
        //Enemy WayPoint �ʱ�ȭ
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
