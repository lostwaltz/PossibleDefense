using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSkip : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(() => CheckWaveSkip());
    }

    public void CheckWaveSkip()
    {
        //Wave가 남아있거나 + 스테이지에 필드 Enemy가 없으면 다음 웨이브로 진행
        if(StageManager.Instance.CurWaveStageData.Count > 0 && StageManager.Instance.CurEnemyCount <= 0)
        {
            StageManager.Instance.WaveSetting();
        }
        else
        {
            Debug.Log("필드에 몹이 남아있습니다.");
        }
    }
}
