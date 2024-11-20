using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_UpgradeButton : UIBase
{
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI level;

    private Button _button;
    public Button _Button { get => _button; set => _button = value; }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void UI_Print(string cost , string level)
    {
        this.cost.text = cost;
        this.level.text = level;
    }

}
