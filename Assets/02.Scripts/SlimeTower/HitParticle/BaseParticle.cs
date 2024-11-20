using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseParticle : MonoBehaviour
{
    [SerializeField] protected float particleLifetimeDuration = 0;
    private Coroutine particleCoroutine;
    private ParticleSystem particle;
    
    
    protected virtual void Awake()
    {
        particle = GetComponent<ParticleSystem>();

        if (particle != null)
        {
            particleLifetimeDuration = particleLifetimeDuration > 0 ? particleLifetimeDuration : particle.main.duration;
            particle.Stop();
        }

        else
        {
            Debug.LogError("ParticleSystem이 존재하지 않습니다!");
        }
    }

    public void Setting(Transform startPos)
    {
        transform.position = startPos.position;
    }

    
    [ContextMenu("파티클 실행")]
    public void StartParticleLifeCycle()
    {
        if (particleCoroutine != null)
        {
            StopCoroutine(particleCoroutine);
        }

        particleCoroutine = StartCoroutine(HandleParticleLifetime());
    }


    private IEnumerator HandleParticleLifetime()
    {
        particle.Play();

        float startTime = Time.time;

        while (particleLifetimeDuration > Time.time - startTime)
        {
            OnParticleUpdate();
            yield return null;
        }

        gameObject.SetActive(false); // 오브젝트 리턴 
    }
    
    
    protected virtual void OnParticleUpdate() //코루틴 라이프사이클 동안 계속 해서 업데이트 됨
    {
    }


    protected  virtual void OnDisable()
    {
        if (particleCoroutine != null)
        {
            StopCoroutine(particleCoroutine);
            particleCoroutine = null;
        }
    }
}