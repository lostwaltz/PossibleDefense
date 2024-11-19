using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

// 장식을 통해서만 Slime을 구분 해주고 있음.  
// 지금은 slime rig와 model이 모두 똑같음 
//rig와 model이 모두 다른 경우에는 어떻게 해야할지 생각해봐야함.
public class BaseSlimeTower : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
    [SerializeField] private Transform firePos;
    [SerializeField] private AttackStrategySO attackStrategyData; //인게임 스탯 변화는 스탯핸들러 통해서 적용 
    [SerializeField] private SlimeTowerDataSO slimeTowerDataSo;  
    public Transform  Target { get;  set; }
    public Animator Animator { get; private set;}
    public SlimeStateMachine SlimeStateMachine { get; private set;}

    public AnimatorHashData animatorHashData = new AnimatorHashData();
    public IAttackStrategy AttackStrategy { get;  private set; }

    public SlimeTowerStatHandler StatHandler { get;  private set;}
    
    //오브젝트 클릭 처리 
    private float _pressStartTime;
    private float _pressDuration;
    private Coroutine _pressCheckCoroutine;


    //타워 판매시 호출 
    private event Action<int> OnTowerSoldEvent; 
    
    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StatHandler = new SlimeTowerStatHandler(slimeTowerDataSo.SlimeTowerStats,slimeTowerDataSo.SlimeTowerUpgradeDataData);
        SlimeStateMachine = new SlimeStateMachine(this);
    }

    private void Start()
    {
        animatorHashData.Initialize();
        SlimeStateMachine.ChangeState(SlimeStateMachine.IdleState);
        
        AttackStrategy = attackStrategyData.GetAttackStrategy();
        AttackStrategy.Setting(firePos,Target,slimeTowerDataSo.SlimeTowerStats.AttackPower);
    }
    
    
    private void Update()
    {
        SlimeStateMachine.Update();
    }

    private void FixedUpdate()
    {
        SlimeStateMachine.FixedUpdateState();
    }

    public void ExecuteTowerSell()
    {
        OnTowerSoldEvent?.Invoke(slimeTowerDataSo.SlimeTowerInfo.sellPrice);
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressStartTime = Time.time; 
        _pressCheckCoroutine = StartCoroutine(CheckPressDuration());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_pressCheckCoroutine != null)
        {
            StopCoroutine(_pressCheckCoroutine);
            _pressCheckCoroutine = null;
        }
    }

    
    
    // POPUP UI 통해서 판매!  
    private IEnumerator CheckPressDuration()
    {
        while (true)
        {
            //TODO 매직넘버 변경하기 
            if (Time.time - _pressStartTime >= 0.3f)
            {
                TowerController.Instance.SetSlimeTower(gameObject);
                 yield break;  
            }
            yield return null;  
        }
    }

}