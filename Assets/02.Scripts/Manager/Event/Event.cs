using System;

public class EventBase : EventArgs
{

}

namespace Achievement
{
    public class EventAchievement : EventBase
    {
        public Achievement.Action Action;
        public Achievement.Target Target;
    
        public float ProgressValue;

        public EventAchievement(Action action, Target target, float progressValue)
        {
            Action = action;
            Target = target;
            ProgressValue = progressValue;
        }
    }
    
    public class EventAchievementWithId : EventAchievement
    {
        public int Id;
        
        public EventAchievementWithId(Action action, Target target, float progressValue, int id) : base(action, target, progressValue)
        {
            Action = action;
            Target = target;
            ProgressValue = progressValue;
            Id = id;
        }
    }
}