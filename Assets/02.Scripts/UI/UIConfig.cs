using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIConfig : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;


    private void Awake()
    {
        SoundManager sm = SoundManager.Instance;
        
        bgmSlider.onValueChanged.AddListener(sm.SetBgmVolume);
        effectSlider.onValueChanged.AddListener(sm.SetEffectVolume);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveAllListeners();
        effectSlider.onValueChanged.RemoveAllListeners();
    }
}
