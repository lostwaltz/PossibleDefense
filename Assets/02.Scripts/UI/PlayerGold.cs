using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGold : UIBase
{
    [SerializeField] private TextMeshProUGUI GoldText;

    private void Awake()
    {
        SetGold();
        GameManager.Instance.GoldChanged -= SetGold;
        GameManager.Instance.GoldChanged += SetGold;
    }

    public void SetGold()
    {
        GoldText.text = GameManager.Instance.PlayerGold.ToString() + "G";
    }
}
