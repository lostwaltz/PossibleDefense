using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIReslut : MonoBehaviour
{
    [SerializeField] private TMP_Text clearTitle;
    [SerializeField] private TMP_Text totalClearWave;
    [SerializeField] private TMP_Text resultGold;
    [SerializeField] private Button lobbyButton;

    private void Start()
    {
        lobbyButton.onClick.AddListener(OnClick);
    }

    public void UpdateUI(bool isClear, int totalWave, int gold)
    {
        clearTitle.text = isClear ? "스테이지 클리어!" : "클리어 실패";
        
        totalClearWave.text = totalWave.ToString();
        
        resultGold.text = gold.ToString();
    }

    private void OnClick()
    {
        Instantiate(Resources.Load<Fader>("UI/UIFade")).
            FadeTo(0f, 1f, 0.3f).
            OnComplete(() => SceneLoadManager.Instance.LoadScene("LobbyScene"));
    }
}
