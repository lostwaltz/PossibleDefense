using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Transform[] wayPoints;
    private int curWayPointIndex = -1;
    private float speed;
    private Vector3 dir;

    public void SetUp(Transform[] waypoints , EnemySO data)
    {
        this.wayPoints = waypoints;
        speed = data.baseSpeed * data.speedModifier;
    }

    public void ChangeSpeed(float amount)
    {
        speed = speed * amount;
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        if (wayPoints == null || wayPoints.Length == 0 || curWayPointIndex == -1) return;

        // 현재 타겟 웨이포인트 가져오기
        Transform targetWayPoint = wayPoints[curWayPointIndex];

        // 방향 계산
        dir = (targetWayPoint.position - transform.position).normalized;

        // 이동
        transform.Translate(Time.deltaTime * speed * dir, Space.World);

        // 웨이포인트 도달 체크
        if ((targetWayPoint.position - transform.position).sqrMagnitude < 0.04f)
        {
            // 다음 웨이포인트로 전환
            curWayPointIndex = (curWayPointIndex + 1) % wayPoints.Length;

            // 회전 설정 (바라보는 방향으로 바로 설정)
            Vector3 lookDirection = wayPoints[curWayPointIndex].position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDisable()
    {
        curWayPointIndex = -1;
    }
}
