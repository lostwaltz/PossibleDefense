using System;
using UnityEngine;



//인 게임에서 슬라임 스탯을 다루기 위한 클래스
public class SlimeTowerStatHandler
{
    private SlimeTowerStatUpgradeData _upgradeData;
    private SlimeTowerStats _stats;
    public float AttackPower { get; private set;}
    public float AttackRange { get; private set;}
    public float AttackSpeed { get; private set;}

    public event Action OnIncreaseStatEvent;
    
    //Option 데이터 기본 값 1 
    public SlimeTowerStatHandler(SlimeTowerStats stats , SlimeTowerStatUpgradeData  upgradeData)
    {
        _upgradeData =  upgradeData ;
        _stats = stats;
        AttackPower = stats.AttackPower * _upgradeData.AttackOptionPower;
        AttackRange = stats.AttackRange * _upgradeData.AttackOptionRange;
        AttackSpeed = stats.AttackSpeed * _upgradeData.AttackOptionSpeed;
        upgradeData.OnUpgradeEvent += IncreaseStats;
    }

    
    //SO 변경 없이 스탯을 저장 해보자 생각.
    // SlimeTowerStatUpgradeData라는 stat 증가 비율 및 OnUpgradeEvent를 가지고 있는 SO 생성
    // 해당 SO를 SlimeTowerDataSO에 넣어두고 SlimeTowerStatHandler의 생성자를 통해 전달 
    // 본인 upgradeData의 이벤트에 스탯 증가 메서드 등록
    
    //TowerUpgradeController를 통해 업그레이드 관리
    // UpgradeData를 인스펙터 창에서 세팅 해주고 이를 버튼리스너에 달아서 버튼이 클릭되면 해당 이벤트 발행 
    // 본인 upgradeData와 맞는 이벤트가 발행되면 IncreaseStats를 통해 본인만의 스탯 증가 
    
    //문제점 
    //업그레이드 이벤트가 일어나면 IncreaseStats를 통해 기존에 소환된 타워의 스탯을 증가 시켜주긴 하지만
    //새롭게 소환된 타워의 경우에는 반영되지 않음.
    
    //스탯 스크립터블 오브젝트에 옵션 벨류 변수를 넣어서 
    // 

    
    public void IncreaseStats()
    {
        AttackPower = _stats.AttackPower * _upgradeData.AttackOptionPower;
        AttackSpeed = _stats.AttackSpeed * _upgradeData.AttackOptionSpeed;
        OnIncreaseStatEvent?.Invoke();
    }

    
}