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
        var modifier = Mathf.Pow(1 + data.RangeUp * 0.1f, 2);
        data.TowersData.SlimeTowerStats.AttackRange *= modifier;
        data.RangeUp = (int)(data.RangeUpgradeGold * modifier);
        data.RangeUp++;
    }

    public override bool IsMaxLevel()
    {
        return data.RangeUp >= data.MaxRangeUp;
    }

}
