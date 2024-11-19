using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private Transform Target;
    private Vector3 offset = new Vector3(0, 0.1f, 0);
    private float duration;

    private bool isAlive = true;

    private void FixedUpdate()
    {
        if (isAlive)
        {
            if (!Target.gameObject.activeSelf)  //when target is dead
            {
                CancelInvoke(nameof(ReturnToPool));
                ReturnToPool();
            }

            this.transform.position = Target.position + offset;
        }
    }

    public void Initialize(Transform target, float duration)
    {
        particle.Stop();
        
        isAlive = true;
        this.Target = target;
        this.transform.position = Target.position + offset;
        Invoke(nameof(ReturnToPool), duration);

        particle.Play();
    }

    private void ReturnToPool()
    {
        particle.Stop();
        isAlive = false;
        gameObject.SetActive(false);
    }
}
