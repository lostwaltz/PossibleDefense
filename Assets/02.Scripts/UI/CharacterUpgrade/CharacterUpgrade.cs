using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public SlimeTowerDataSO TowersData;
    public GameObject CharacterPrefab;

    public int SpeedUp;
    public int PowerUp;
    public int RangeUp;

    public void SpeedUpgrade()
    {

    }
    public void PowerUpgrade()
    {

    }
    public void RangeUpgrade()
    {

    }
}

public class CharacterUpgrade : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> UpgradeDataList;

    private int curIndex = 0;

    public void OnEnable()
    {
        gameObject.SetActive(true);
        ShowCharacter(curIndex);
    }

    public void NextButton()
    {
        curIndex = (curIndex + 1) % UpgradeDataList.Count;
        ShowCharacter(curIndex);
    }

    public void PreviousButton()
    {
        if(curIndex.Equals(0))
        {
            curIndex = UpgradeDataList.Count;
        }
        curIndex = (curIndex - 1) % UpgradeDataList.Count;
        ShowCharacter(curIndex);
    }

    private void OffAllCharacter()
    {
        foreach( var data in UpgradeDataList)
        {
            data.CharacterPrefab.SetActive(false);
        }
    }

    private void ShowCharacter(int index)
    {
        OffAllCharacter();
        UpgradeDataList[index].CharacterPrefab.gameObject.SetActive(true);
    }
}
