using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIConfig : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider effectSlider;

    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = SoundManager.Instance;
        

    }

    private void OnEnable()
    {
        bgmSlider.onValueChanged.AddListener(_soundManager.SetBgmVolume);
        effectSlider.onValueChanged.AddListener(_soundManager.SetEffectVolume);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveAllListeners();
        effectSlider.onValueChanged.RemoveAllListeners();
    }
}
