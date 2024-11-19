using TMPro;
using UnityEngine;

public class UI_EnemyCount  : MonoBehaviour //UIBase
{
    [SerializeField] private TextMeshProUGUI enemyCount;
    public void UIPrint(float waveTime, int wave, int enemyCount)
    {
        this.enemyCount.text = "Enemy Count : " + enemyCount.ToString();
    }
}
