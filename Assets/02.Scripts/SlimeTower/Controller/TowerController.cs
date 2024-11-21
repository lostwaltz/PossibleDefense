using System.Collections;
using UnityEngine;

public class TowerController : Singleton<TowerController>
{
    private GameObject _selectedTower;
    //[SerializeField] private Transform _targetTile;

    [SerializeField] private TowerTile _targetTile;
    [SerializeField] private TowerAttackRangeIndicator _attackRangeIndicator;

    protected override void Awake()
    {
        base.Awake();
        _attackRangeIndicator = PoolManagerForTest.Instance.poolLegacy.SpawnFromPool("RangeDisplayIndicator")
            .GetComponent<TowerAttackRangeIndicator>();
    }

    public void SetSlimeTower(GameObject slimeTower)
    {
        if (slimeTower.GetComponent<BaseSlimeTower>().IsWalking)
            return;

        if (_selectedTower == slimeTower)
        {
            _attackRangeIndicator.OffAttackRangeIndicator();
            _selectedTower = null;
            return;
        }

        _selectedTower = slimeTower;
        _attackRangeIndicator.OnAttackRangeIndicator(_selectedTower.transform,
            _selectedTower.GetComponent<BaseSlimeTower>().StatHandler.AttackRange);
    }

    //public void SetTargetTile(Transform tile)
    //{
    //    if (_selectedTower == null)
    //        return;

    //    _targetTile = tile;
    //    MoveSlimeTower();
    //}

    public void SetTargetTile(TowerTile tile)
    {
        if (_selectedTower == null || tile.SlimeTower != null)
            return;
        
        _targetTile = tile;
        MoveSlimeTower();
        _attackRangeIndicator.OffAttackRangeIndicator();
    }

    public void MoveSlimeTower()
    {
        var stateMachine = _selectedTower.GetComponent<BaseSlimeTower>().SlimeStateMachine;
        stateMachine.WalkState.target = _targetTile;
        stateMachine.ChangeState(stateMachine.WalkState);

        _selectedTower = null;
    }
}