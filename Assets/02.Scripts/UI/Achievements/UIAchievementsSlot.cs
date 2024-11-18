using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIAchievementsSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    
    public void UpdateUI(Achievement.AchievementData data)
    {
        nameText.text = data.name;
        descriptionText.text = data.description;
        fillImage.fillAmount = data.currentValue / data.targetValue;
        button?.gameObject.SetActive(fillImage.fillAmount >= 1f);
        
        icon.sprite = data.icon;
    }

    private void Awake()
    {
        button.onClick.AddListener(TakeReward);
    }

    private void TakeReward()
    {
        button.onClick.RemoveAllListeners();
        Destroy(button.gameObject);
        button = null;
    }
}
