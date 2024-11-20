using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BaseHitParticle : MonoBehaviour
{
    [SerializeField] private float particleLifetimeDuration;
    protected int _targetLayerMask = LayerMask.GetMask("Enemy");
    protected Collider[] _results = new Collider[50]; //상수 ? 사용 

    protected void OnEnable()
    {
        StartCoroutine(HandleParticleLifetime());
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("파티클 수행");
    }

    // protected abstract void 

    protected IEnumerator HandleParticleLifetime()
    {
        float startTime = Time.time;

        while (particleLifetimeDuration > Time.time - startTime)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}