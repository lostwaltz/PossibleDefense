using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장식을 통해서만 Slime을 구분 해주고 있음.  
// 지금은 slime rig와 model이 모두 똑같음 
//rig와 model이 모두 다른 경우에는 어떻게 해야할지 생각해봐야함.
public class BaseSlimeTower : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject[] slimeDecoArray;
    [SerializeField] private AttackStrategySO _attackStrategyData;
    
    public SlimeTowerStatSo slimeTowerData;
    private SlimeStateMachine _slimeStateMachine;

    public Transform  Target { get;  set; }
    public Animator animator { get; private set;}
    public AnimatorHashData animatorHashData = new AnimatorHashData();
    public IAttackStrategy AttackStrategy { get;  private set; }
    
    
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log(slimeTowerData.SlimeTowerStats.AttackSpeed);
        _slimeStateMachine = new SlimeStateMachine(this);
    }

    private void Start()
    {
        animatorHashData.Initialize();
        _slimeStateMachine.ChangeState(_slimeStateMachine.IdleState);
        
        AttackStrategy = _attackStrategyData.GetAttackStrategy();
        AttackStrategy.Setting(firePos,Target,slimeTowerData.SlimeTowerStats.AttackPower);
    }
    
    
    private void Update()
    {
        _slimeStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _slimeStateMachine.FixedUpdateState();
    }
    
    private void SetSlimeTowerData(SlimeTowerStatSo _slimeTowerData)
    {
        slimeTowerData = _slimeTowerData;
    }
    

    
}