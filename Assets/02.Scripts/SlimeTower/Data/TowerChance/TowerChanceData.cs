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

        var grades = Enum.GetValues(typeof(TowerGrade));

        foreach (TowerGrade grade in grades)
        {
            float probability = 0f; 

            foreach (var towerChance in towerChances)
            {
                if (towerChance.towerGrade == grade)
                {
                    probability = towerChance.probability;
                    break; 
                }
            }

            chanceList.Add(probability);
        }

        return chanceList;
    }

    
    
}