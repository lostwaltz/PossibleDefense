using System.Collections;
using System.Collections.Generic;
using Achievement;
using Achievement.SO;
using UnityEngine;

public class LobbyScene : SceneBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        var fader = Object.Instantiate(Resources.Load<Fader>("UI/UIFade"));
        fader.FadeTo(1f, 0f, 0.3f).OnComplete( fader.Release );;

        if (null != SceneLoadManager.PrevScene) return;
        
        AchievementManager.Instance.Init();
    }

}
