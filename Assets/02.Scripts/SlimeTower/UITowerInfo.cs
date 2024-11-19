using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//TODO UIBase를 상속받아 처리 
public class UITowerInfo : MonoBehaviour
{
    [SerializeField] private BaseSlimeTower _slimeTower;
    [SerializeField] private Button sellButton;


    private void Awake()
    {
        sellButton.onClick.AddListener(OnSellButton);
    }
    
    private void OnSellButton()
    {
        _slimeTower.ExecuteTowerSell();
    }
}