using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BaseHitParticle : MonoBehaviour
{
    [SerializeField] private float particleLifetimeDuration;
    protected int _targetLayerMask = LayerMask.GetMask("Enemy");
    protected Collider[] _results = new Collider[50]; //��� ? ��� 

    protected void OnEnable()
    {
        StartCoroutine(HandleParticleLifetime());
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("��ƼŬ ����");
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