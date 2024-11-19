using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : SceneBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        
        Debug.Log("EnterLobbyScene");
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Debug.Log("ExitLobbyScene");
    }
}
