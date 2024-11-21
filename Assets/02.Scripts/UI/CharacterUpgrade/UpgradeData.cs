using System;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public SlimeTowerDataSO TowersData;
    public GameObject CharacterPrefab;

    public int SpeedUp;
    public int PowerUp;
    public int RangeUp;

    public int MaxSpeedUp = 10;
    public int MaxPowerUp = 10;
    public int MaxRangeUp = 10;

    public int SpeedUpgradeGold = 100;
    public int PowerUpgradeGold = 100;
    public int RangeUpgradeGold = 100;

    public float originSpeed;
    public float originPower;
    public float originRange;

    public SpeedUpgrade SpeedUpgrade { get; private set; }
    public PowerUpgrade PowerUpgrade { get; private set; }
    public RangeUpgrade RangeUpgrade { get; private set; }

    public string GetTowerName()
    {
        return TowersData.towerName;
    }

    public void ResetSO()
    {
        TowersData.SlimeTowerStats.AttackSpeed = originSpeed;
        TowersData.SlimeTowerStats.AttackPower = originPower;
        TowersData.SlimeTowerStats.AttackRange = originRange;
        Debug.Log($"reset {TowersData.towerName}");
    }

    public void Init()
    {
        originSpeed = TowersData.SlimeTowerStats.AttackSpeed;
        originPower = TowersData.SlimeTowerStats.AttackPower;
        originRange = TowersData.SlimeTowerStats.AttackRange;

        SpeedUpgrade = new SpeedUpgrade(this);
        PowerUpgrade = new PowerUpgrade(this);
        RangeUpgrade = new RangeUpgrade(this);
    }
}
