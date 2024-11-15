using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

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

    //0 : EnemyTile  , 1 : Player Tile  , 2 : EnemyMoveStart , 3: EnemyMoveFinisi , 4: EnemyMoveStartFinish
    public int[,] boardMatrix =
    {
        {0,0,0,2},
        {0,1,1,1},
        {0,1,1,1},
        {0,0,0,3},
    };

    private void Awake()
    {
        GameObject Tile = new GameObject();
        for (int y = 0; y < Row; y++)
        {
            for (int x = 0; x < Column; x++)
            {
                switch (boardMatrix[y, x])
                {
                    case 0:
                        Tile = Instantiate(EnemyTile, transform);
                        break;
                    case 1:
                        Tile = Instantiate(PlayerTile, transform);
                        break;
                    case 2:
                        Tile = Instantiate(EnemyMoveStartTile, transform);
                        break;
                    case 3:
                        Tile = Instantiate(EnemyMoveEndTile, transform);
                        break;
                }

                Tile.transform.position = new Vector3((x * 5) + SpacingColumn + offsetColumn, 0, (-y * 5) + SpacingRow + offsetRow);
            }
        }
        }

      
}
