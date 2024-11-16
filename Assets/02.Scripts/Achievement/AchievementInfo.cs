using System;

namespace Achievement
{
    [System.Serializable]
    public class AchievementInfo
    {
        public string name;
        public string description;

        public float targetValue;
        
        public Achievement.Action action;
        public Achievement.Target target;
    }
}