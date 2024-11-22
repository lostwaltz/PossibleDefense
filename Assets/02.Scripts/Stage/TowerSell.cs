using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//타워 판매 기능 클래스 
public class TowerSell : MonoBehaviour
{
    [SerializeField] private GameObject SellTowerPanel;
    [SerializeField] private Button SellButton;
    [SerializeField] private BaseSlimeTower target; //판매하는 타워 

    [SerializeField] private TextMeshProUGUI attackStatText;
    [SerializeField] private TextMeshProUGUI speedStatText;
    [SerializeField] private TextMeshProUGUI rangeStatText;

    private void Awake()
    {
        SellButton.onClick.AddListener(() => SellTower());
    }

    public void OnEnable()
    {
        attackStatText.text = "0";
        speedStatText.text = "0";
        rangeStatText.text = "0";
    }

    public void SellTowerTarget(GameObject _target) //판매모드에서 타워 클릭시 저정 되는 메서드
    {
        target = _target.GetComponent<BaseSlimeTower>();
        StageManager.Instance.Stage.SelectTileClear();
        StageManager.Instance.Stage.TowerTiles[target.CurTowerTileIndex].Select.SetActive(true);
        attackStatText.text = target.StatHandler.AttackPower.ToString("N2");
        speedStatText.text = target.StatHandler.AttackSpeed.ToString("N2");
        rangeStatText.text = target.StatHandler.AttackRange.ToString("N2");
    }

    public void SellTower() //버튼을 누르면 타워가 판매됨
    {
        if (target != null)
        {
            StageManager.Instance.CurTowerCount--;

            StageManager.Instance.Stage.TowerTiles[target.CurTowerTileIndex].SlimeTower = null; // 스테이지의 타일 데이터 초기화
            target.CurTowerTileIndex = -1; //타워의 타일 Index 초기화

             StageManager.Instance.CurGold += target.TestExecuteTowerSell();
        }
    }
}

