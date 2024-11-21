using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeUpgrade : BaseUpgrade
{
    public RangeUpgrade(UpgradeData data) : base(data)
    {
    }

    public override int GetUpgradeCost()
    {
        return data.RangeUpgradeGold;
    }

    public override void Upgrade()
    {
        var modifier = data.RangeUp++;
        data.TowersData.SlimeTowerStats.AttackRange += modifier;
        data.RangeUpgradeGold = (int)(data.RangeUpgradeGold * modifier);
        
    }

    public override bool IsMaxLevel()
    {
        return data.RangeUp >= data.MaxRangeUp;
    }

}
