using UnityEngine;
using UnityEngine.EventSystems;

public class TowerTile : BaseTile
{
    public GameObject SlimeTower = null;

    private int index;
    public int Index {  get { return index; } set { index = value; } }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //타일 클릭하여 이동시에 해당 타일에 Tower가 없는경우에만 이동가능
        if (!StageManager.Instance.IsSellMode)
        {
            TowerController.Instance.SetTargetTile(this);
        }
    }
}
