using System;
using UnityEngine;



//인 게임에서 슬라임 스탯을 다루기 위한 클래스
public class SlimeTowerStatHandler
{
    private SlimeTowerStatUpgradeData _upgradeData;

    public float AttackPower { get; private set;}
    public float AttackRange { get; private set;}
    public float AttackSpeed { get; private set;}

    public event Action OnIncreaseStatEvent;
    
    public SlimeTowerStatHandler(SlimeTowerStats stats , SlimeTowerStatUpgradeData  upgradeData)
    {
        _upgradeData =  upgradeData ;
        AttackPower = stats.AttackPower;
        AttackRange = stats.AttackRange;
        AttackSpeed = stats.AttackSpeed;

        upgradeData.OnUpgradeEvent += IncreaseStats;
    }


    public void IncreaseStats()
    {
        AttackPower *= _upgradeData.InGameAttackPowerIncrease;
        AttackSpeed *= _upgradeData.InGameAttackSpeedIncrease;
        OnIncreaseStatEvent?.Invoke();
    }

    
}