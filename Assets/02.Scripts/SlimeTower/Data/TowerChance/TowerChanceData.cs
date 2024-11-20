using System;
using System.Collections.Generic;
using UnityEngine;

public enum TowerGrade
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "TowerChanceData", menuName = "Tower Data/Tower Chance Data")]
public class TowerChanceData : ScriptableObject
{
    [Serializable]
    public class TowerChance
    {
        public TowerGrade towerGrade;  
        public float probability;
    }

    public List<TowerChance> towerChances = new List<TowerChance>();

    
    public List<float> GetChanceList()
    {
        List<float> chanceList = new List<float>();
        foreach (var towerChance in towerChances)
        {
            chanceList.Add(towerChance.probability);
        }
        return chanceList;
    }

    
    public Dictionary<TowerGrade, float> GetChanceDictionary()
    {
        Dictionary<TowerGrade, float> chanceDictionary = new Dictionary<TowerGrade, float>();
        foreach (var towerChance in towerChances)
        {
            chanceDictionary[towerChance.towerGrade] = towerChance.probability;
        }
        return chanceDictionary;
    }
    
}