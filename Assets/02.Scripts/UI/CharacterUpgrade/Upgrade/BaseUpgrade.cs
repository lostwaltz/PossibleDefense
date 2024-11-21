using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
    void Upgrade();
    int GetUpgradeCost();
    bool CanUpgrade(int gold);
    bool IsMaxLevel();
}

public abstract class BaseUpgrade : IUpgradeable
{
    protected UpgradeData data;

    public BaseUpgrade(UpgradeData data)
    {
        this.data = data;
    }

    public abstract int GetUpgradeCost();

    public abstract void Upgrade();

    public bool CanUpgrade(int gold)
    {
        return gold >= GetUpgradeCost() && !IsMaxLevel();
    }

    public abstract bool IsMaxLevel();
}
