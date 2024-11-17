//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


////���� �߿��� <���� ���� �ȿ� ���� ã�� ���>�� ������ �ȵ�
////�̵��߿��� �����ؾ��� 
//public class TargetSearch : MonoBehaviour
//{
//    [SerializeField] private float radius; //�� Ÿ�� Ž�� �Ÿ� 
//    [SerializeField] private GameObject shortEnemyTarget;
//    [SerializeField] private LayerMask layer; //Ÿ������ ������ ���̾� ����
//    private Collider[] colliders;

//    public float Radius { get => radius; set => radius = value; }
//    public GameObject ShortEnemyTarget { get => shortEnemyTarget; }

//    private void Update()
//    {
//        OnTargetSearch();
//    }

//    public void OnTargetSearch()
//    {
//        colliders = Physics.OverlapSphere(transform.position, radius, layer);

//        if (colliders.Length > 0)
//        {
//            float short_distance = Vector3.Distance(transform.position, colliders[0].transform.position);
//            foreach (Collider col in colliders)
//            {
//                float short_distance2 = Vector3.Distance(transform.position, col.transform.position);
//                if (short_distance >= short_distance2)
//                {
//                    short_distance = short_distance2;
//                    shortEnemyTarget = col.gameObject;
//                }
//            }
//            return;
//        }

//        shortEnemyTarget = null;
//    }

//    public Transform TargetToDirection()
//    {
//        if (ShortEnemyTarget != null)
//        {
//            return ShortEnemyTarget.transform;
//        }

//        return null;
//    }


//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, radius);
//    }
//}