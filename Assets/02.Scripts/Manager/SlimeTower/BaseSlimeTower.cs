using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 장식을 통해서만 Slime을 구분 해주고 있음.  
// 지금은 slime rig와 model이 모두 똑같음 
//rig와 model이 모두 다른 경우에는 어떻게 해야할지 생각해봐야함.
public class BaseSlimeTower : MonoBehaviour
{
    [SerializeField] private Transform Target;

    [SerializeField] private GameObject[] slimeDecoArray;
    private SlimeTowerStatSo slimeTowerData;

    private IAttackStrategy _strategy;
    
    private SlimeStateMachine slimeStateMachine;

    
    private void Start()
    {
        _strategy = new SingleTargetAttackByProjectile(transform,Target);
        _strategy.Execute();
    }

    private void OnEnable()
    {
    }

    private void SetSlimeTowerData(SlimeTowerStatSo _slimeTowerData)
    {
        slimeTowerData = _slimeTowerData;
    }

    private void SetSlimeAppearance()
    {
        foreach (var deco in slimeDecoArray)
        {
            deco.SetActive(false);
        }

        //TODO 어드레서블 혹은 리소시스에 넣어두고 id 값에 맞게 가져와서 사용하면 case 없어도 됨!
        switch (slimeTowerData.SlimeTowerData.id)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                Debug.LogWarning("Unknown slime ID");
                break;
        }
    }

    

    private void OnDisable()
    {
        slimeTowerData = null;
    }
}