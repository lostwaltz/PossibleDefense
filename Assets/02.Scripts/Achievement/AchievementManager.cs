using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Achievement
{
    public class AchievementManager : SingletonDontDestroy<AchievementManager>
    {
        public AchievementInfoContainer achievementInfoContainer;
        private readonly Dictionary<Action, Dictionary<Target, List<AchievementData>>> _achievementDictionary = new();
        
        private void Start()
        {
            EventManager.Instance.Subscribe<EventAchievement>(EventManager.Channel.Achievement, OnAchievement);

            foreach (var info in achievementInfoContainer.achievementsDataList)
                GetOrAddList(info.action, info.target).Add(new AchievementData(info));
        }

        private void OnAchievement(EventAchievement data)
        {
            foreach (AchievementData achievementData in GetOrAddList(data.Action, data.Target))
                achievementData.CurrentValue += data.ProgressValue;
        }

        private List<AchievementData> GetOrAddList(Action action, Target target)
        {
            if(false == _achievementDictionary.TryGetValue(action, out var achievementsDictionary))
                _achievementDictionary[action] = new Dictionary<Target, List<AchievementData>>();

            if (false == _achievementDictionary[action].TryGetValue(target, out var achievementsList))
                _achievementDictionary[action][target] = achievementsList = new List<AchievementData>();

            return achievementsList;
        }
    }
}