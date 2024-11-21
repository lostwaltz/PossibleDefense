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
        var modifier = data.PowerUp++;   //ratio
        data.TowersData.SlimeTowerStats.AttackPower += modifier;    //caculate stat
        data.PowerUpgradeGold = (int)(data.PowerUpgradeGold * modifier);    //caculate gold
        
    }
}
