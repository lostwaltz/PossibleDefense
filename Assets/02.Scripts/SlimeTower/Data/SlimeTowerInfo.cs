
using UnityEngine;
using UnityEngine.Serialization;


//변하지 않는 데이터 값들만 저장
[CreateAssetMenu(fileName = "SlimeTowerData", menuName = "SlimeTower/SlimeTowerData")]
public class SlimeTowerInfo : ScriptableObject
{
     public int id;
     public int grade;
     public int sellPrice;
}