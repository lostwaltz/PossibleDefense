using System;
using UnityEngine;
using UnityEngine.UI;


public class TowerUpgradeController : MonoBehaviour
{
    [SerializeField] private SlimeTowerStatUpgradeData  [] _upgradeDatas ;
    [SerializeField] private Button button;
    [SerializeField] private Button button2;

    private void Awake()
    {
        button.onClick.AddListener(()=>UpgradeTowerByGrade(TowerGrade.Common));
        button2.onClick.AddListener(()=>UpgradeTowerByGrade(TowerGrade.Epic));
    }

    //버튼 누르면 해당 등급에 따라 강화 
    public void UpgradeTowerByGrade(TowerGrade grade)
    {
        foreach (var data in _upgradeDatas)
        {
            if (data.Grade == grade)
            {
                data.OnUpgrade();
                break;
            }
        }
    }

}