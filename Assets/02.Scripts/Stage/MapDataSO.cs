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
    public int Row;//�� , ����
    public int Column;//�� ,����

    public RowLine[] MapMatrix;

    public List<Vector3> WayPoint; // ���߿��� Queue�� �ۼ��Ͽ� �������� �Ҽ� �ְ� ����Ұ�
    public List<Vector3> PlayerTilePoint; // �÷��̾ ��ġ�� �� �ִ� Ÿ�� 
}
