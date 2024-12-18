using UnityEngine;
using UnityEngine.UI;

public class UI_SellModeBtn : UIBase
{
    private bool isToggled; // 버튼 상태
    private Button button; // 버튼 참조
    private Image buttonImage; // 버튼 이미지 

    [SerializeField] private GameObject SellTowerBax;
    [SerializeField] private Color toggledColor = Color.green;  // 눌렸을 때 색상
    [SerializeField] private Color defaultColor = Color.white;  // 기본 색상

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>(); // 버튼의 Image 컴포넌트 가져오기
        button.onClick.AddListener(OnButtonClick); // 클릭 이벤트 연결
    }

    private void OnButtonClick()
    {
        isToggled = !isToggled; // 상태 전환
        StageManager.Instance.Stage.SelectTileClear();

        if (isToggled)
        {
            StageManager.Instance.IsSellMode = true;
            SellTowerBax.SetActive(true);
            // 눌린 상태
            buttonImage.color = toggledColor;
            Debug.Log("Button is toggled ON");
        }
        else
        {
            StageManager.Instance.IsSellMode = false;
            SellTowerBax.SetActive(false);

            // 기본 상태
            buttonImage.color = defaultColor;
            Debug.Log("Button is toggled OFF");
        }
    }
}