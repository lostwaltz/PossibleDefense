using System.Collections;
using UnityEngine;

public class TowerController : Singleton<TowerController>
{
    private GameObject _selectedTower;
    [SerializeField] private Transform _targetTile;

    public void SetSlimeTower(GameObject slimeTower)
    {
        _selectedTower = slimeTower;
    }
    
    
    public void SetTargetTile(Transform tile)
    {
        if (_selectedTower == null)
            return;
        
        _targetTile = tile;
        MoveSlimeTower();
    }
    
    public void MoveSlimeTower()
    {
        
        var stateMachine = _selectedTower.GetComponent<BaseSlimeTower>().SlimeStateMachine;
        stateMachine.WalkState.target = _targetTile;
        stateMachine.ChangeState(stateMachine.WalkState);
        
        _selectedTower = null;
    }
    
}