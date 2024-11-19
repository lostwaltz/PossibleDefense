using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_WaveIndicator : MonoBehaviour //UIBase
{
    [SerializeField] private TextMeshProUGUI waveTime;
    [SerializeField] private TextMeshProUGUI wave;
    [SerializeField] private TextMeshProUGUI enemyCount;

    StringBuilder strbuilder = new StringBuilder();

    public void UIPrint(float waveTime , int wave , int enemyCount)
    {
        this.waveTime.text = "Timer : "+ waveTime.ToString();
        this.wave.text = "Wave : " + wave.ToString();
        this.enemyCount.text = "Enemy Count : " + enemyCount.ToString();
    }
}
