using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Transform[] wayPoints;
    private int curWayPointIndex = 0;
    private float speed;
    private Vector3 dir;
    private Transform targetWayPoint;

    private bool isDead = false;

    public void SetUp(Transform[] waypoints, EnemySO data)
    {
        this.wayPoints = waypoints;
        speed = data.baseSpeed * data.speedModifier;

        // 초기 웨이포인트 설정
        curWayPointIndex = 0;
        targetWayPoint = wayPoints[curWayPointIndex];
        UpdateDirection();

        //몬스터죽음 이벤트에 OnDead함수 등록해서 몬스터가 죽으면 움직이지 않도록
        //모든 몬스터가 한번에 멈추는걸 방지하기위해 제네릭으로 EnemyMovement를하여 인수로 this를?
    }

    private void UpdateDirection()
    {
        if (targetWayPoint != null)
        {
            dir = (targetWayPoint.position - transform.position).normalized;
        }
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (wayPoints == null || wayPoints.Length == 0 || isDead)
            return;

        // 이동
        transform.Translate(Time.deltaTime * speed * dir, Space.World);

        // 웨이포인트 도달 체크
        if (IsCloseToPoint(targetWayPoint.position))
        {
            // 다음 웨이포인트로 전환
            curWayPointIndex = (curWayPointIndex + 1) % wayPoints.Length;
            targetWayPoint = wayPoints[curWayPointIndex];

            // 방향 업데이트
            UpdateDirection();
        }

        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDead()
    {
        isDead = true;
    }


    private bool IsCloseToPoint(Vector3 point)
    {
        // 거리가 0.2 이하이면 도달한 것으로 간주
        return (point - transform.position).sqrMagnitude < 1f;
    }

    private void OnDisable()
    {
        curWayPointIndex = 0;
        targetWayPoint = null;
        isDead = false;
        //이벤트 등록한거 해제해주기
    }
}

