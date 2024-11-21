using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpgrade : BaseUpgrade
{
    public PowerUpgrade(UpgradeData data) : base(data)
    {
    }

    public override int GetUpgradeCost()
    {
        return data.PowerUpgradeGold;
    }

    public override bool IsMaxLevel()
    {
        return data.PowerUp >= data.MaxPowerUp;
    }

    public override void Upgrade()
    {
        var modifier = Mathf.Pow(1 + data.PowerUp * 0.1f, 2);   //ratio
        data.TowersData.SlimeTowerStats.AttackPower *= modifier;    //caculate stat
        data.PowerUpgradeGold = (int)(data.PowerUpgradeGold * modifier);    //caculate gold
        data.PowerUp++;
    }
}
