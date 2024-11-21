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
        private Dictionary<int, List<AchievementData>>[,] _achievementDataArray;
        private EventManager _eventManager;

        public int AchievementCount { get; private set; }

        public void Init()
        {
            achievementDataContainer = Resources.Load<AchievementDataContainer>("DataSheet/AchievementsData");

            _achievementDataArray = new Dictionary<int, List<AchievementData>>[(int)Action.Count, (int)Target.Count];

            foreach (var info in achievementDataContainer.achievementsDataList)
                GetOrAddList(info.action, info.target, info.targetId).Add(new AchievementData(info));

            AchievementCount = achievementDataContainer.achievementsDataList.Count;
            
            _eventManager = EventManager.Instance;
            EventManager.Instance.Subscribe<EventAchievement>(EventManager.Channel.Achievement, OnAchievement);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                _eventManager.Publish(EventManager.Channel.Achievement, new Achievement.EventAchievement(Action.Kill, Target.Monster, 1f, 100));
        }

        private void OnAchievement(EventAchievement data)
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

            _achievementDataArray[actionIndex, targetIndex] ??= new Dictionary<int, List<AchievementData>>();

             if (!_achievementDataArray[actionIndex, targetIndex].TryGetValue(key, out var achievementDataList))
                _achievementDataArray[actionIndex, targetIndex][key] =
                    achievementDataList = new List<AchievementData>();

            return achievementDataList;
        }

        public AchievementData[] GetAchievementData()
        {
            List<AchievementData> achievementDataList = new();

            foreach (var dataDictionary in _achievementDataArray)
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