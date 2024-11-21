using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : UIBase
{
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
            if(i < GameManager.Instance.curClearStageNum + 1)
            {
                //현재까지 클리어한 스테이지는 번호로 표시
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
