using UnityEngine;
using UnityEngine.Serialization;


//변하지 않는 데이터 값들만 저장
[CreateAssetMenu(fileName = "SlimeTowerData", menuName = "SlimeTower/SlimeTowerData")]
public class SlimeTowerGradeInfo : ScriptableObject
{
    public TowerGrade grade;
    public int sellPrice;
}