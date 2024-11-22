using System;
using System.Collections.Generic;
using Achievement.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Achievement
{
    public class AchievementManager : SingletonDontDestroy<AchievementManager>
    {
        private SO.AchievementDataContainer achievementDataContainer;
        private Dictionary<int, List<AchievementData>>[,] achievementDataArray;
        private EventManager eventManager;

        public int AchievementCount { get; private set; }

        public void Init()
        {
            achievementDataContainer = Resources.Load<AchievementDataContainer>("DataSheet/AchievementsData");
            
            achievementDataArray = new Dictionary<int, List<AchievementData>>[(int)Action.Count, (int)Target.Count];

            foreach (var info in achievementDataContainer.achievementsDataList)
                GetOrAddList(info.action, info.target, info.targetId).Add(new AchievementData(info));

            AchievementCount = achievementDataContainer.achievementsDataList.Count;
            
            eventManager = EventManager.Instance;
            EventManager.Instance.Subscribe<EventAchievement>(EventManager.Channel.Achievement, OnTriggerAchievement);
        }
        private void OnTriggerAchievement(EventAchievement data)
        {
            foreach (AchievementData achievementData in GetOrAddList(data.Action, data.Target, data.TargetId))
            {
                achievementData.IncrementValue(data.ProgressValue);

                Debug.Log(achievementData.currentValue);
            }

            if (0 == data.TargetId) return;

            foreach (AchievementData achievementData in GetOrAddList(data.Action, data.Target, 0))
            {
                achievementData.IncrementValue(data.ProgressValue);

                Debug.Log(achievementData.currentValue);
            }
        }

        private List<AchievementData> GetOrAddList(Action action, Target target, int key)
        {
            var actionIndex = (int)action;
            var targetIndex = (int)target;

            achievementDataArray[actionIndex, targetIndex] ??= new Dictionary<int, List<AchievementData>>();

             if (!achievementDataArray[actionIndex, targetIndex].TryGetValue(key, out var achievementDataList))
                achievementDataArray[actionIndex, targetIndex][key] =
                    achievementDataList = new List<AchievementData>();

            return achievementDataList;
        }

        public AchievementData[] GetAchievementData()
        {
            List<AchievementData> achievementDataList = new();

            foreach (var dataDictionary in achievementDataArray)
            {
                if (null == dataDictionary) continue;

                foreach (var keyValuePair in dataDictionary)
                {
                    foreach (AchievementData data in keyValuePair.Value)
                        achievementDataList.Add(data);
                }
            }

            return achievementDataList.ToArray();
        }
    }
}
