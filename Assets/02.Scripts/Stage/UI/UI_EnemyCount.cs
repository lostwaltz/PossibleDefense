using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyCount  : UIBase
{
    [SerializeField] private TextMeshProUGUI enemyCount;
    [SerializeField] private TextMeshProUGUI deathCount;
    [SerializeField] private Image enemyCountGauge;
    public void UIPrint(int DeathCount, int enemyCount)
    {
        enemyCountGauge.rectTransform.localScale = new Vector3((float)enemyCount / (float)DeathCount, 1, 1);
        this.enemyCount.text = enemyCount.ToString();
        this.deathCount.text = DeathCount.ToString();
    }
}
