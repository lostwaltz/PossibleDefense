using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RowLine
{
    public int[] index; // 피드백 : int 데이터를 enum으로 변경해서 사용할것 
}

[CreateAssetMenu(fileName = "MapDataSO", menuName = "MapDataSO/Default", order = 0)]
public class MapDataSO : ScriptableObject
{

    public int Row;//열 , 세로
    public int Column;//행 ,가로

    public RowLine[] MapMatrix; // 피드백 : int 데이터를 enum으로 변경해서 사용할것 

    public List<Vector3> WayPoint; // 나중에는 Queue로 작성하여 동적생성 할수 있게 사용할거
    public List<Vector3> PlayerTilePoint; // 플레이어가 배치할 수 있는 타일 
}
