using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : BaseUpgrade
{
    public SpeedUpgrade(UpgradeData data) : base(data)
    {
    }

    public override int GetUpgradeCost()
    {
        return data.SpeedUpgradeGold;
    }

    public override void Upgrade()
    {
        var modifier = Mathf.Pow(1 + data.SpeedUp * 0.1f, 2);   //ratio
        data.TowersData.SlimeTowerStats.AttackSpeed *= modifier;    //caculate stat
        data.SpeedUpgradeGold = (int)(data.SpeedUpgradeGold * modifier);    //caculate gold
        data.SpeedUp++;
    }

    public override bool IsMaxLevel()
    {
        return data.SpeedUp >= data.MaxSpeedUp;
    }

}
