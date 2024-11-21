using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSaveContainer
{
    public List<UpgradeSaveData> saveDatas = new List<UpgradeSaveData>();
    public int curClearStage;
    public int playerGold;
}

[System.Serializable]
public class UpgradeSaveData
{
    public string TowerName;

    public int SpeedUp;
    public int PowerUp;
    public int RangeUp;

    public int SpeedUpgradeGold;
    public int PowerUpgradeGold;
    public int RangeUpgradeGold;

    public float originSpeed;
    public float originPower;
    public float originRange;
}
