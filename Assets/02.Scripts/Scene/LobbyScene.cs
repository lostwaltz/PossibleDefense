using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : SceneBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        var fader = Object.Instantiate(Resources.Load<Fader>("UI/UIFade"));
        fader.FadeTo(1f, 0f, 0.3f);
        
        Debug.Log("EnterLobbyScene");
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Debug.Log("ExitLobbyScene");
    }
}
