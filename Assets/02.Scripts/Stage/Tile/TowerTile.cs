using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : BaseTile
{
    //private BaseSlimeTower SlimeTower = null;

    public override void OnPointerClick(PointerEventData eventData)
    {
        TowerController.Instance.SetTargetTile(transform);
        Debug.Log($"isTower : {isTower}");
        Debug.Log($"isTower : {transform.position}");
    }
}
