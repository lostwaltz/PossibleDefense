using UnityEngine;


//인 게임에서 슬라임 스탯을 다루기 위한 클래스
public class SlimeTowerStatHandler 
{

    public SlimeTowerStats Stats { get; private set;}
    public SlimeTowerInfo Info { get; private set;}


    public SlimeTowerStatHandler(SlimeTowerStats stats , SlimeTowerInfo info)
    {
        Stats = stats;
        Info = info;
    }


}