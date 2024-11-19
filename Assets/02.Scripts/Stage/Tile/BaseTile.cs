using UnityEngine.EventSystems;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour, IPointerClickHandler 
{
    protected bool isTower = false; // 타워가 설치되어있는지 체크하는 변수
    
    public bool IsTower { get => isTower; set => isTower = value; }

    public abstract void OnPointerClick(PointerEventData eventData);

}

