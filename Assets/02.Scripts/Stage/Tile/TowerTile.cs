using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : BaseTile
{
    //private BaseSlimeTowe SlimeTower = null;
    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"isTower : {isTower}");
        Debug.Log($"isTower : {transform.position}");
    }
}
