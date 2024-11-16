using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed; //����ü �ӵ�
    public Vector3 Direction; //����ü ���� 
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
            Debug.Log("�� ���� �ޤ���");
            Destroy(gameObject);
        }
    }
}
