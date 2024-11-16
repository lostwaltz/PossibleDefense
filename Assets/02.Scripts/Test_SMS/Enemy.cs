using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform Target; //도착지점
    public float Speed; //캐릭터 노가다 
    
    public List<Vector3> WayPoint; // 나중에는 Queue로 작성하여 동적생성 할수 있게 사용할거
    public int WayIndex; //경로 인덱스

    private void Update()
    {
        if (Vector3.Distance(WayPoint[WayIndex], transform.position) <= 0.1f)
        {
            if(WayIndex == WayPoint.Count - 1)//끝 인덱스에 도달한경우
            {
                WayIndex = 0;
            }
            else
            {
                WayIndex++;
            }
        }
        else
        {
            Vector3 dir = (WayPoint[WayIndex] - transform.position).normalized;

            transform.position += dir * Speed * Time.deltaTime;
        }
        
    }



}
