namespace Achievement
{
    public class AchievementData
    {
        public AchievementData(AchievementInfo baseInfo)
        {
            BaseInfo = baseInfo;
            TargetValue = baseInfo.targetValue;
            CurrentValue = 0f;
        }
        
        public AchievementInfo BaseInfo;
        public float TargetValue;
        public float CurrentValue;
    }
}