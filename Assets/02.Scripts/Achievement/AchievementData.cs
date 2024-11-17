using UnityEngine;

namespace Achievement
{
    [System.Serializable]
    public class AchievementData
    {
        public AchievementData(AchievementData achievementData)
        {
            name = achievementData.name;
            description = achievementData.description;
            
            target = achievementData.target;
            action = achievementData.action;
            
            targetValue = achievementData.targetValue;
            targetId = achievementData.targetId;
            
            isReset = achievementData.isReset;
            
            currentValue = 0;
        }
        [HideInInspector] public float currentValue;
        
        public string name;
        public string description;
        
        public float targetValue;
        public int  targetId;

        public bool isReset;
        
        public Action action;
        public Target target;
    }
}