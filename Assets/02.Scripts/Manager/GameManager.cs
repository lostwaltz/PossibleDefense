using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : SingletonDontDestroy<GameManager>
{
    public int CallStageNum; // 스테이지 넘버
    public int PlayerGold; // 로비에서 사용되는 플레이어 골드

    protected override void Awake()
    {
        base.Awake();
    }
}
