using UnityEngine;


// Slime Stats SO를 초기화 해주기 위한 클래스
public class SlimeTowerStatController : MonoBehaviour
{
    private SlimeTowerStats[] _slimeTowerStatsArray;


    //Json or CSV 값을 Load해 초기화
    public void InitStatData()
    {
        foreach (var stat in _slimeTowerStatsArray)
        {
            // 나중에는 데이터 로드해와서 해당 ID값에 맞게 초기화 
            stat.AttackPower = 10f;  
            stat.AttackPower = 0.5f; 
        }   
    }


}