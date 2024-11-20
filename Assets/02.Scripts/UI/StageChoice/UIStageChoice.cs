using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : UIBase
{
    //CSV 파일 갯수만큼의 자물쇠 표시
    //현재 클리어한 최대 스테이지가 몇인지 표시
    //최대 클리어한 스테이지 + 1까지만 Stage N

    [SerializeField] private StageButton StageButton;
    [SerializeField] private GameObject StageLocked;
    [SerializeField] private GameObject GridLayout;

    private int numOfStage = 1;
    private int curStage = 1;   //StageManager에서 Get

    private void Awake()
    {
        Init();
        gameObject.SetActive(false);
    }


    private void Init()
    {
        numOfStage = Resources.LoadAll("StageDB/MapMatrixDB").Length;
        SetButtons();
    }

    private void SetButtons()
    {
        for(int i = 0; i < numOfStage; i++)
        {
            if(i < curStage)
            {
                StageButton btn = Instantiate(StageButton, GridLayout.transform);
                btn.SetButton(i);
            }
            else
            {
                Instantiate(StageLocked, GridLayout.transform);
            }
        }
    }
}
