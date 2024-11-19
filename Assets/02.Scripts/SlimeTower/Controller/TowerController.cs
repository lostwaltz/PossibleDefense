using System.Collections;
using UnityEngine;

public class TowerController : Singleton<TowerController>
{
    //���õ� Ÿ���� ����
    private GameObject _selectedTower;
    [SerializeField] private Transform _targetTile;
    //Mouse ��ǥ�� �޾Ƽ� ����  ,Ray ��� 

    //Mouse OnClick  
    public void SetSlimeTower(GameObject slimeTower)
    {
        _selectedTower = slimeTower;
        Debug.Log("������ ����");
    }
    
    //EmptyTile 
    
    public void SetTargetTile(Transform tile)
    {
        if (_selectedTower == null)
            return;
        _targetTile = tile;
        MoveSlimeTower();
    }


    [ContextMenu("������ ������ �׽�Ʈ")]
    public void MoveSlimeTower()
    {
        var stateMachine = _selectedTower.GetComponent<BaseSlimeTower>().SlimeStateMachine;
        stateMachine.WalkState.target = _targetTile;
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    
    // OnClick - 1�ʹ̸� info UI  , 1�� �̻� movement  
    
    //Case 1: info UI  info UI�� ������ 

    //tower ��ȯ�� ���� ���� ?   - TowerSpawner�� ���� ��ȯ! 

    //tile ������ �޾ƿ� �� �ִ� �κ��� �ʿ���. �ش� ��ġ�� tower�� ��ġ ���ִ°�?  -> StageManager�� ���� �޾ƿ� ������ ?
    
    // tile ���� ���� ��ü - stageManager  // �̰� Ȯ���ϱ� ���� ����Ʈ? 
    
    //���� �����͸� ��ȯ�� �� �־�� towerController���� Event = > stageManager���� �޾Ƽ� ó��   
    
    //Case 2: Movement �÷��̾ ������ Ÿ�Ϸ� ������ �� �ֵ��� ���� 
    //��ġ�� ���ٸ� �̵� ���� ��ġ ���ִٸ� �̵� �ȵ� 
    // �����̴°� Ȯ�� �Ǳ� �������� stateMachine �ǵ��� ����! 
    
    //�� �� Ŭ�� ��
    // selectedTower nulló��
    //UpdateUI selectedTower�� ���õ� UI ������. 
    //ObjectPool Get ������Ű�� �� Ÿ�� ���� �޾Ƽ� spawn 
    //�̵������ ��쿡�� ���� ���� Ÿ���� �� Ÿ�������� �� �� �־����! 
    
    // �� Ÿ������ �ƴ��� - StageManager  
    // �ó׸ӽ� ī�޶� ������  õ�忡�� �����ٺ��� �������� �����ؼ� 
    // Ÿ�� Ŭ���ϸ� �� ������ �̵� 
}