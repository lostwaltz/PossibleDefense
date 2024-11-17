using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Achievement
{
    public class AchievementManager : SingletonDontDestroy<AchievementManager>
    {
        public AchievementInfoContainer achievementInfoContainer;
        private Dictionary<int, List<AchievementData>>[,] _achievementDataArray;
        private EventManager _eventManager;
        
        private void Start()
        {
            _achievementDataArray = new Dictionary<int, List<AchievementData>>[(int)Action.Count, (int)Target.Count];
            
            EventManager.Instance.Subscribe<EventAchievement>(EventManager.Channel.Achievement, OnAchievement);

            foreach (var info in achievementInfoContainer.achievementsDataList)
                GetOrAddList(info.action, info.target, info.targetId).Add(new AchievementData(info));
            
            _eventManager = EventManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _eventManager.Publish(EventManager.Channel.Achievement, new EventAchievement(Achievement.Action.Kill, Achievement.Target.Monster, 1f));
            if (Input.GetKeyDown(KeyCode.Escape))
                _eventManager.Publish(EventManager.Channel.Achievement, new EventAchievement(Achievement.Action.Kill, Achievement.Target.Cash, 1f, 1000));
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
            
            _achievementDataArray[actionIndex, targetIndex] ??= new Dictionary<int, List<AchievementData>>();
            
            if (!_achievementDataArray[actionIndex, targetIndex].TryGetValue(key, out var achievementDataList))
                _achievementDataArray[actionIndex, targetIndex][key] = achievementDataList = new List<AchievementData>();
            
            return achievementDataList;
        }
    }
}