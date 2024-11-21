using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UpgradeType
{
    Speed,
    Power,
    Range
}

public class CharacterUpgrade : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> UpgradeDataList;
    [SerializeField] private TextMeshProUGUI nameLabel;

    [SerializeField] private TextMeshProUGUI SpeedGold; 
    [SerializeField] private TextMeshProUGUI SpeedUpgrade;
    [SerializeField] private TextMeshProUGUI PowerGold;
    [SerializeField] private TextMeshProUGUI PowerUpgrade;
    [SerializeField] private TextMeshProUGUI RangeGold;
    [SerializeField] private TextMeshProUGUI RangeUpgrade;

    private int curIndex = 0;
    private int LobbyGold = 10000;

    private void Awake()
    {
        Init();
    }

    public void OnEnable()
    {
        ShowCharacter(curIndex);
    }

    public void ResetButtonText()
    {
        SpeedGold   .text = UpgradeDataList[curIndex].SpeedUpgradeGold.ToString() + "G";
        SpeedUpgrade.text = UpgradeDataList[curIndex].SpeedUp.ToString();
        PowerGold   .text = UpgradeDataList[curIndex].PowerUpgradeGold.ToString() + "G";
        PowerUpgrade.text = UpgradeDataList[curIndex].PowerUp.ToString();
        RangeGold   .text = UpgradeDataList[curIndex].RangeUpgradeGold.ToString() + "G";
        RangeUpgrade.text = UpgradeDataList[curIndex].RangeUp.ToString();
    }

    [ContextMenu("ResetTowerUpgrade")]
    public void ResetSOs()
    {
        foreach(var data in UpgradeDataList)
        {
            data.ResetSO();
        }
    }

    public void Init()
    {
        foreach (var data in UpgradeDataList)
        {
            data.Init();
        }
    }

    public void NextButton()
    {
        curIndex = (curIndex + 1) % UpgradeDataList.Count;
        ShowCharacter(curIndex);
    }

    public void PreviousButton()
    {
        if (curIndex.Equals(0))
        {
            curIndex = UpgradeDataList.Count;
        }
        curIndex = (curIndex - 1) % UpgradeDataList.Count;
        ShowCharacter(curIndex);
    }

    private void OffAllCharacter()
    {
        foreach (var data in UpgradeDataList)
        {
            data.CharacterPrefab.SetActive(false);
        }
        nameLabel.text = string.Empty;
    }

    private void ShowCharacter(int index)
    {
        OffAllCharacter();
        nameLabel.text = UpgradeDataList[index].GetTowerName();
        UpgradeDataList[index].CharacterPrefab.gameObject.SetActive(true);
        ResetButtonText();
    }

    private void ExecuteUpgrade(IUpgradeable upgrade)
    {
        if (!upgrade.CanUpgrade(LobbyGold)) return;

        LobbyGold -= upgrade.GetUpgradeCost();
        upgrade.Upgrade();
    }

    public void UpgradeSpeed()
    {
        ExecuteUpgrade(UpgradeDataList[curIndex].SpeedUpgrade);
        ResetButtonText();
    }
    public void UpgradePower()
    {
        ExecuteUpgrade(UpgradeDataList[curIndex].PowerUpgrade);
        ResetButtonText();
    }
    public void UpgradeRange()
    {
        ExecuteUpgrade(UpgradeDataList[curIndex].RangeUpgrade);
        ResetButtonText();
    }
}
