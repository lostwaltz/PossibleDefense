using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RowLine
{
    public int[] index;
}

[CreateAssetMenu(fileName = "MapDataSO", menuName = "MapDataSO/Default", order = 0)]
public class MapDataSO : ScriptableObject
{
    public int Row;//열 , 세로
    public int Column;//행 ,가로

    public RowLine[] MapMatrix;

    public List<Vector3> WayPoint; // 나중에는 Queue로 작성하여 동적생성 할수 있게 사용할거
    public List<Vector3> PlayerTilePoint; // 플레이어가 배치할 수 있는 타일 
}
