using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class BaseSlimeTower : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // --- SerializeField 변수들 (인스펙터 설정) ---
    [SerializeField] private Transform firePos; 
    [SerializeField] private AttackStrategySO attackStrategyData; 
    [SerializeField] private SlimeTowerDataSO slimeTowerDataSo; 

    // --- 공개 속성 ---
    public Transform Target { get; set; } 
    public Animator Animator { get; private set; } 
    public SlimeStateMachine SlimeStateMachine { get; private set; } 
    public IAttackStrategy AttackStrategy { get; private set; } 
    public SlimeTowerStatHandler StatHandler { get; private set; } 

    // --- 읽기 전용 데이터 ---
    public readonly AnimatorHashData AnimatorHashData = new AnimatorHashData(); 
    
    // --- IPointer 관련 변수들 ---
    private float _pressStartTime; // 클릭 시작 시간
    private float _pressDuration; // 클릭 지속 시간
    private Coroutine _pressCheckCoroutine; // 클릭 체크 코루틴

    // --- 이벤트 ---
    private event Action<int> OnTowerSoldEvent; // 타워 판매 시 호출되는 이벤트


    private void Awake()
    {
        Animator = GetComponent<Animator>();
        StatHandler =
            new SlimeTowerStatHandler(slimeTowerDataSo.SlimeTowerStats, slimeTowerDataSo.SlimeTowerUpgradeDataData);
        SlimeStateMachine = new SlimeStateMachine(this);
    }

    private void Start()
    {
        AnimatorHashData.Initialize();
        SlimeStateMachine.ChangeState(SlimeStateMachine.IdleState);

        AttackStrategy = attackStrategyData.GetAttackStrategy();
        AttackStrategy.Setting(firePos, Target, slimeTowerDataSo.SlimeTowerStats.AttackPower);
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
        Debug.Log("판매 가격" + slimeTowerDataSo.SlimeTowerInfo.sellPrice);
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