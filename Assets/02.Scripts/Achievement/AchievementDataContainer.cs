using System.Collections.Generic;
using UnityEngine;



namespace Achievement
{
    namespace SO
    {
        [CreateAssetMenu(fileName = "AchievementsData", menuName = "AchievementsData/AchievementsData", order = int.MaxValue)]
        public class AchievementDataContainer : ScriptableObject
        {
            public List<AchievementData> achievementsDataList;
        }
    }
}