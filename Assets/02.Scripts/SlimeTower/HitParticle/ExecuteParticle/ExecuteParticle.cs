using System.Collections;
using UnityEngine;

public abstract class ExecuteParticle : BaseParticle, IExecute
{
    protected bool IsExecute = false; // 한번만 수행 시 필요한 플래그 변수 

    protected void OnEnable()
    {
        IsExecute = false;
    }

    public abstract void Execute();
   

    protected override void OnParticleUpdate()
    {
        Execute();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        IsExecute = false;
    }
}