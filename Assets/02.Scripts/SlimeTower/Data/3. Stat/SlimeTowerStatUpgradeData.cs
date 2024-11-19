using System;
using UnityEngine;
 
[CreateAssetMenu(fileName = "SlimeTowerStatUpgrade", menuName = "SlimeTower/Data/SlimeTowerStatUpgrade")]
public class SlimeTowerStatUpgradeData : ScriptableObject
{
    public TowerGrade Grade;
    
    [SerializeField] public float InGameAttackSpeedIncrease;
    [SerializeField] public float InGameAttackPowerIncrease;
    [SerializeField] public float InGameAttackRangeIncrease;

    public event Action OnUpgradeEvent;

    public void OnUpgrade()
    {
        Debug.Log(Grade + " 능력치 증가");
        OnUpgradeEvent?.Invoke();
    }
}