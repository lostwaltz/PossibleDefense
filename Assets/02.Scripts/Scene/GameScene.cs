using UnityEngine;


public class GameScene : SceneBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        var fader = Object.Instantiate(Resources.Load<Fader>("UI/UIFade"));
        fader.FadeTo(1f, 0f, 0.3f);
        
        Debug.Log("EnterMainScene");
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Debug.Log("ExitMainScene");
    }
}