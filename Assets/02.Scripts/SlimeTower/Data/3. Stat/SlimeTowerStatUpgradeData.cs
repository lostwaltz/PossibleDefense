using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeTowerStatUpgrade", menuName = "SlimeTower/Data/SlimeTowerStatUpgrade")]
public class SlimeTowerStatUpgradeData : ScriptableObject
{
    //등급 관련이 있음
    public TowerGrade Grade;

    //등급별 스탯 증가률 
    [SerializeField] public float InGameAttackSpeedIncrease;
    [SerializeField] public float InGameAttackPowerIncrease;
    [SerializeField] public float InGameAttackRangeIncrease;

    public float AttackOptionSpeed = 1;
    public float AttackOptionPower = 1;
    public float AttackOptionRange = 1;

    public event Action OnUpgradeEvent;

    //강화 버튼이 눌리면 실행되는 메서드 
    // 슬라임 타워의 등급은 동일해도 stats data는 다를 수 있음.
    // stat안에 옵션데이터가 들어가 있으니깐 

    public void OnUpgrade()
    {
        AttackOptionSpeed *= InGameAttackSpeedIncrease; 
        AttackOptionPower *= InGameAttackPowerIncrease;
        OnUpgradeEvent?.Invoke();
    }

    
    
    [ContextMenu("데이터 초기화")]
    public void Init()
    {
        AttackOptionSpeed = 1;
        AttackOptionPower = 1;
    }

    // 처음 시작시 Reset 불러주기 
    public void Reset()
    {
        Init();
     }

    //빌드하고 나면 so값이 변경되도 저장이 안됨 
    //그래서 읽기용으로 사용하는거임 플레이 중에 거쳐가는 느낌으로 
    
    //씬에서 어떤 오브젝트도 스크립터블 오브젝트를 참조 하지 않으면 초기화 됨. 
    //reset 메서드 존재
    
    
    //여기서 기존 스탯을 따로 증가 시켜주는 로직 적용 ? 
    //그리고 나중에 해제해주는 로직도 적용 
    //기존 스탯을 복사 해두고 SO 값 증가 그리고 초기화 시켜주기 
}