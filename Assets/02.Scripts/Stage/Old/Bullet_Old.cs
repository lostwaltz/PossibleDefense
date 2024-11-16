using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed; //투사체 속도
    public Vector3 Direction; //투사체 방향 
    public Transform Target;
    public LayerMask EnemyLayerMask;

    public void Initialize(Transform target)
    {
        this.Target = target;
    }

    private void Update()
    {
        Vector3 dir = (Target.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(EnemyLayerMask == (1<<other.gameObject.layer | EnemyLayerMask))
        {
            Debug.Log("적 공격 받ㅂ음");
            Destroy(gameObject);
        }
    }
}
