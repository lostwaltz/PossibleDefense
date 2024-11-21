using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageTowerUpgradeLevel
{
    public TowerGrade Grade;
    public int Level;
    public int Cost;

    public StageTowerUpgradeLevel(TowerGrade Grade, int Level, int Cost)
    {
        this.Grade = Grade;
        this.Level = Level;
        this.Cost = Cost;
    }
}

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private SlimeTowerStatUpgradeData[] _upgradeDatas;
    [SerializeField] private UI_UpgradeButton[] buttons;
    [SerializeField] private int[] gradeCost;

    [SerializeField] private int maxUpgradeLevel = 10;
    [SerializeField] private float upgradeModifier = 1.2f;
    private Dictionary<UI_UpgradeButton, StageTowerUpgradeLevel> upgradeButtonMappings;

    private void Start()
    {
        upgradeButtonMappings = new Dictionary<UI_UpgradeButton, StageTowerUpgradeLevel>
        {
            { buttons[0], new StageTowerUpgradeLevel(TowerGrade.Common, 1, gradeCost[0]) },
            { buttons[1], new StageTowerUpgradeLevel(TowerGrade.Rare, 1, gradeCost[1]) },
            { buttons[2], new StageTowerUpgradeLevel(TowerGrade.Epic, 1, gradeCost[2]) },
        };
        

        foreach (KeyValuePair<UI_UpgradeButton, StageTowerUpgradeLevel> pair in upgradeButtonMappings)
        {
            pair.Key._Button.onClick.AddListener(() => UpgradeTowerByGrade(pair.Value));
            pair.Key._Button.onClick.AddListener(() => 
            pair.Key.UI_Print(
                 maxUpgradeLevel == pair.Value.Level ? "0" : pair.Value.Cost.ToString(), 
                maxUpgradeLevel == pair.Value.Level ? StageUpgradeConstain.MaxUpgrade : pair.Value.Level.ToString()) 
            ) ; //UI 출력 로직 

        }
    }

    //버튼 누르면 해당 등급에 따라 강화 
    public void UpgradeTowerByGrade(StageTowerUpgradeLevel upgradeTarget)
    {
        if (StageManager.Instance.UseGold(upgradeTarget.Cost) && maxUpgradeLevel > upgradeTarget.Level)
        {
            foreach (SlimeTowerStatUpgradeData data in _upgradeDatas)
            {
                if (data.Grade == upgradeTarget.Grade)
                {
                    upgradeTarget.Level++;
                    upgradeTarget.Cost = Mathf.FloorToInt(upgradeTarget.Cost * upgradeModifier);
                    data.OnUpgrade();
                    break;
                }
            }
        }
    }
}