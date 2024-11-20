using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;

public class UpgradesController : MonoBehaviour 
{
    [SerializeField] private SlimeTowerStatUpgradeData[] _upgradeDatas;
    [SerializeField] private Button[] buttons;

    private Dictionary<Button, TowerGrade> upgradeButtonMappings;

    private void Awake()
    {
        upgradeButtonMappings = new Dictionary<Button, TowerGrade>
        {
            { buttons[0], TowerGrade.Common},
            { buttons[1], TowerGrade.Rare},
            { buttons[2], TowerGrade.Epic},            
        };

        foreach(KeyValuePair<Button, TowerGrade> pair in upgradeButtonMappings)
        {
            pair.Key.onClick.AddListener(() => UpgradeTowerByGrade(pair.Value));
        }
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
