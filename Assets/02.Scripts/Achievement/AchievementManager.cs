using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Achievement
{
    public class AchievementManager : SingletonDontDestroy<AchievementManager>
    {
        public AchievementInfoContainer achievementInfoContainer;
        private readonly Dictionary<Action, Dictionary<Target, Dictionary<int, List<AchievementData>>>> _achievementDictionary = new();

        private Dictionary<int, List<AchievementData>>[,] _achievementDataArray;
        
        
        private void Start()
        {
            var actionLength = Enum.GetValues(typeof(Action)).Length;
            var targetLength = Enum.GetValues(typeof(Target)).Length;
            _achievementDataArray = new Dictionary<int, List<AchievementData>>[actionLength, targetLength];
            
            EventManager.Instance.Subscribe<EventAchievement>(EventManager.Channel.Achievement, OnAchievement);

            foreach (var info in achievementInfoContainer.achievementsDataList)
                GetOrAddList(info.action, info.target, info.targetId).Add(new AchievementData(info));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventManager.Instance.Publish(EventManager.Channel.Achievement, new EventAchievement(Achievement.Action.Kill, Achievement.Target.Monster, 1f));
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EventManager.Instance.Publish(EventManager.Channel.Achievement, new EventAchievement(Achievement.Action.Kill, Achievement.Target.Monster, 1f, 100));
            }
        }

        private void OnAchievement(EventAchievement data)
        {
            foreach (AchievementData achievementData in GetOrAddList(data.Action, data.Target, data.TargetId))
            {
                achievementData.currentValue += data.ProgressValue;
                
                Debug.Log(achievementData.currentValue);
            }
            
            if(0 == data.TargetId) return;
            
            foreach (AchievementData achievementData in GetOrAddList(data.Action, data.Target, 0))
            {
                achievementData.currentValue += data.ProgressValue;
                
                Debug.Log(achievementData.currentValue);
            }
        }

        private List<AchievementData> GetOrAddList(Action action, Target target, int key)
        {
            var actionIndex = (int)action;
            var targetIndex = (int)target;

            if (_achievementDataArray[actionIndex, targetIndex].TryGetValue(key, out var achievementDataList))
                _achievementDataArray[actionIndex, targetIndex][key] = achievementDataList = new List<AchievementData>();
            
            
            if (false == _achievementDictionary.TryGetValue(action, out var achievementsTargetDictionary))
                _achievementDictionary[action] = new Dictionary<Target, Dictionary<int, List<AchievementData>>>();

            if (false == _achievementDictionary[action].TryGetValue(target, out var achievementsKeyDic))
                _achievementDictionary[action][target] = new Dictionary<int, List<AchievementData>>();

            if (false == _achievementDictionary[action][target].TryGetValue(key, out var achievementsList))
                _achievementDictionary[action][target][key] = achievementsList = new List<AchievementData>();

            return achievementsList;
        }
    }
}