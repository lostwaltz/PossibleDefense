using System;

public class EventBase : EventArgs
{

}

namespace Achievement
{
    public class EventAchievement : EventBase
    {
        public readonly Achievement.Action Action;
        public readonly Achievement.Target Target;
    
        public readonly float ProgressValue;
        public readonly int TargetId;

        public EventAchievement(Action action, Target target, float progressValue, int targetId = 0)
        {
            Action = action;
            Target = target;
            ProgressValue = progressValue;
            TargetId = targetId;
        }
    }

}