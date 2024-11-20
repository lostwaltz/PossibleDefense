using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParticle : BaseParticle
{
    private Transform Target;
    private Vector3 offset = new Vector3(0, 0.1f, 0);


    protected void Awake()
    {
        base.Awake();
    }

    protected override void OnParticleUpdate()
    {
        base.OnParticleUpdate();
        this.transform.position = Target.position + offset;
    }

    public override void Setting(Transform target)
    {
        particle.Stop();

        this.Target = target;
        StartParticleLifeCycle();
    }
}
