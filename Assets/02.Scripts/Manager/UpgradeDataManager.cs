using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UpgradeDataManager
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "upgrade_data.json");

    public static void SaveUpgradeData(List<UpgradeData> upgradeDataList)
    {
        var container = new UpgradeSaveContainer();
        foreach(var upgradeData in upgradeDataList)
        {
            var saveData = new UpgradeSaveData()
            {
                TowerName = upgradeData.TowersData.towerName,

                SpeedUp = upgradeData.SpeedUp,
                PowerUp = upgradeData.PowerUp,
                RangeUp = upgradeData.RangeUp,

                SpeedUpgradeGold = upgradeData.SpeedUpgradeGold,
                PowerUpgradeGold = upgradeData.PowerUpgradeGold,
                RangeUpgradeGold = upgradeData.RangeUpgradeGold,

                originSpeed = upgradeData.originSpeed,
                originPower = upgradeData.originPower,
                originRange = upgradeData.originRange
            };
            container.saveDatas.Add(saveData);
        }
        container.curClearStage = GameManager.Instance.curClearStageNum;
        container.playerGold = GameManager.Instance.PlayerGold;

        var json = JsonUtility.ToJson(container, true);
        File.WriteAllText(SavePath, json);
        Debug.Log(SavePath);
    }

    public static void LoadUpgradeData(List<UpgradeData> upgradeDataList)
    {
        if (!File.Exists(SavePath))
        {
            return;
        }

        var json = File.ReadAllText(SavePath);
        var container = JsonUtility.FromJson<UpgradeSaveContainer>(json);

        foreach (var saveData in container.saveDatas)
        {
            var upgradeData = upgradeDataList.Find(data => data.TowersData.towerName == saveData.TowerName);
            if (upgradeData != null)
            {
                upgradeData.SpeedUp = saveData.SpeedUp;
                upgradeData.PowerUp = saveData.PowerUp;
                upgradeData.RangeUp = saveData.RangeUp;

                upgradeData.SpeedUpgradeGold = saveData.SpeedUpgradeGold;
                upgradeData.PowerUpgradeGold = saveData.PowerUpgradeGold;
                upgradeData.RangeUpgradeGold = saveData.RangeUpgradeGold;

                upgradeData.originSpeed = saveData.originSpeed;
                upgradeData.originPower = saveData.originPower;
                upgradeData.originRange = saveData.originRange;
            }
        }

        GameManager.Instance.curClearStageNum = container.curClearStage;
        GameManager.Instance.PlayerGold = container.playerGold;
    }
}
