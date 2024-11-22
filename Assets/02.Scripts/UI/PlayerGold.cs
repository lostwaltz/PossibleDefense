using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGold : UIBase
{
    [SerializeField] private TextMeshProUGUI GoldText;

    private void Awake()
    {

    }

    public void SetGold()
    {
        GoldText.text = GameManager.Instance.PlayerGold.ToString() + "G";
    }
}
