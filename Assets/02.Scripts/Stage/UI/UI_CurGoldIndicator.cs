using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CurGoldIndicator : UIBase
{
    [SerializeField] private TextMeshProUGUI curGold;

    public void UIPrint(int curGold)
    {
        this.curGold.text = curGold.ToString();
    }
}
