using System.Collections.Generic;
using UnityEngine;

namespace Achievement
{
    [CreateAssetMenu(fileName = "AchievementsData", menuName = "AchievementsData/AchievementsData", order = int.MaxValue)]
    public class AchievementInfoContainer : ScriptableObject
    {
        public List<AchievementData> achievementsDataList;
    }
}