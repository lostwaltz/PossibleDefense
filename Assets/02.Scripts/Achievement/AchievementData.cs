using UnityEngine;
using UnityEngine.Serialization;

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

            icon = achievementData.icon;
            ;

            isReset = achievementData.isReset;

            currentValue = 0;
        }

        [HideInInspector] public float currentValue;

        public string name;
        public string description;

        public float targetValue;
        public int targetId;

        public bool isReset;

        public Sprite icon;


        public Action action;
        public Target target;

        public void IncrementValue(float value)
        {
            switch (isReset)
            {
                case true when currentValue < value:
                    currentValue = value;
                    break;
                case false:
                    currentValue += value;
                    break;
            }
        }
    }
}