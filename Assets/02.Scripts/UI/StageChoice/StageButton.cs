using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI StageNum;
    private Button stageBtn;

    private int StageIndex;

    private void Awake()
    {
        stageBtn = GetComponent<Button>();
    }

    public void SetButton(int index)
    {
        StageIndex = index;
        StageNum.text = $"Stage {index + 1}";
        stageBtn.onClick.AddListener(() => StageStart(index));       
    }

    private void StageStart(int index)
    {
        Debug.Log($"input index : {index}");
        //해당 스테이지번호로 넘어가는 매서드
    }
}
