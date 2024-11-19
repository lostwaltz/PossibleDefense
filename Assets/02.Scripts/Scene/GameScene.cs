using UnityEngine;

public class GameScene : SceneBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        
        Debug.Log("EnterMainScene");
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Debug.Log("ExitMainScene");
    }
}