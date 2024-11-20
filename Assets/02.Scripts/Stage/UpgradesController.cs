using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;

[System.Serializable]
public class StageTowerUpgradeLevel
{
    public TowerGrade Grade;
    public int Level;

    public StageTowerUpgradeLevel(TowerGrade Grade, int Level)
    {
        this.Grade = Grade;
        this.Level = Level;
    }
}

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private SlimeTowerStatUpgradeData[] _upgradeDatas;
    [SerializeField] private Button[] buttons;

    [SerializeField] private int maxUpgradeLevel = 10;
    //private Dictionary<Button, TowerGrade> upgradeButtonMappings;
    private Dictionary<Button, StageTowerUpgradeLevel> upgradeButtonMappings;

    private void Awake()
    {
        upgradeButtonMappings = new Dictionary<Button, StageTowerUpgradeLevel>
        {
            { buttons[0], new StageTowerUpgradeLevel(TowerGrade.Common, 1)},
            { buttons[1], new StageTowerUpgradeLevel(TowerGrade.Normal, 1)},
            { buttons[2], new StageTowerUpgradeLevel(TowerGrade.Epic, 1)},
        };

        foreach (KeyValuePair<Button, StageTowerUpgradeLevel> pair in upgradeButtonMappings)
        {
            TextMeshProUGUI LevelText = pair.Key.GetComponentInChildren<TextMeshProUGUI>();

            pair.Key.onClick.AddListener(() => UpgradeTowerByGrade(pair.Value));
            pair.Key.onClick.AddListener(() => 
            LevelText.text = maxUpgradeLevel == pair.Value.Level ? StageUpgradeConstain.MaxUpgrade : pair.Value.Level.ToString()); //UI 출력 로직 

        }
    }

    //버튼 누르면 해당 등급에 따라 강화 
    public void UpgradeTowerByGrade(StageTowerUpgradeLevel upgradeTarget)
    {
        if (maxUpgradeLevel > upgradeTarget.Level)
        {
            foreach (var data in _upgradeDatas)
            {
                if (data.Grade == upgradeTarget.Grade)
                {
                    upgradeTarget.Level++;
                    data.OnUpgrade();
                    break;
                }
            }
        }
    }
}
