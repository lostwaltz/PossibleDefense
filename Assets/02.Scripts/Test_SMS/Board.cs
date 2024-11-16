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

    public List<Vector3> WayPoint; // ���߿��� Queue�� �ۼ��Ͽ� �������� �Ҽ� �ְ� ����Ұ�
    public List<Vector3> PlayerTilePoint; // �÷��̾ ��ġ�� �� �ִ� Ÿ�� 
}

public class Board : MonoBehaviour
{
    public Enemy[] Enemies; //�ش� ���������� ������ �� ĳ���� , ���߿� ������Ʈ Ǯ������ �����ؾ��� 
    public int EnemyIndex; //���� ���� �ε���

    public GameObject EnemyTile;
    public GameObject PlayerTile;
    public GameObject EnemyMoveStartTile;
    public GameObject EnemyMoveEndTile;

    public int Row;//�� , ����
    public int Column;//�� ,����

    public int SpacingRow; // Ÿ�ϰ� Ÿ�� ���� ���� y��
    public int SpacingColumn; // Ÿ�ϰ� Ÿ�� ���� ���� x��

    public float offsetRow;//�� , ����
    public float offsetColumn;//�� ,����

    public Vector3 StartPosition;
    public Vector3 EndPosition;

    public MapData curMap;//���� ���ӿ��� ����� �� �����͸� �����ϰ� �ִ� Class ������ 
    

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
