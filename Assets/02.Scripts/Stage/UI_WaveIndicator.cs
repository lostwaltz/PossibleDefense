using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_WaveIndicator : MonoBehaviour //UIBase
{
    [SerializeField] private TextMeshProUGUI waveTime;
    [SerializeField] private TextMeshProUGUI wave;

    StringBuilder strbuilder = new StringBuilder();

    public void UIPrint(float waveTime , int wave , int enemyCount)
    {
        int totalTime = (int)waveTime;

        int min = totalTime / 60;
        int second = totalTime % 60;

        strbuilder.Clear();
        strbuilder.Append(min);
        strbuilder.Append(" : ");
        strbuilder.Append(second);

        this.waveTime.text = strbuilder.ToString();
        this.wave.text = wave.ToString();

    }
}
