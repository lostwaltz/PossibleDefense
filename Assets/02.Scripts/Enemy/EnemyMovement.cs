using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Vector3[] wayPoints;
    private int curWayPointIndex = 0;
    private float speed;
    private Vector3 dir;
    private Transform targetWayPoint;

    private Transform model;

    private bool isDead = false;

    public void SetUp(Vector3[] waypoints, EnemySO data, GameObject model)
    {
        this.wayPoints = waypoints;
        speed = data.baseSpeed * data.speedModifier;
        this.model = model.transform;

        // 초기 웨이포인트 설정
        curWayPointIndex = 0;
        targetWayPoint.position = wayPoints[curWayPointIndex];
        UpdateDirection();

        isDead = false;
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
        if(!isDead)
        Move();
    }

    public void Move()
    {
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        // 이동
        transform.Translate(Time.deltaTime * speed * dir, Space.World);

        // 웨이포인트 도달 체크
        if (IsCloseToPoint(targetWayPoint.position))
        {
            // 다음 웨이포인트로 전환
            curWayPointIndex = (curWayPointIndex + 1) % wayPoints.Length;
            targetWayPoint.position = wayPoints[curWayPointIndex];

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
            model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void OnDead()
    {
        isDead = true;
    }


    private bool IsCloseToPoint(Vector3 point)
    {
        // 거리가 0.2 이하이면 도달한 것으로 간주
        return (point - transform.position).sqrMagnitude < 0.04 * speed;
    }

    private void OnDisable()
    {
        curWayPointIndex = 0;
        targetWayPoint = null;
    }
}

