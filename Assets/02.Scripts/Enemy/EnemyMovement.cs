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

    private Transform model;

    private bool isDead = false;

    public void SetUp(Transform[] waypoints, EnemySO data, GameObject model)
    {
        this.wayPoints = waypoints;
        speed = data.baseSpeed * data.speedModifier;
        this.model = model.transform;

        // �ʱ� ��������Ʈ ����
        curWayPointIndex = 0;
        targetWayPoint = wayPoints[curWayPointIndex];
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

        // �̵�
        transform.Translate(Time.deltaTime * speed * dir, Space.World);

        // ��������Ʈ ���� üũ
        if (IsCloseToPoint(targetWayPoint.position))
        {
            // ���� ��������Ʈ�� ��ȯ
            curWayPointIndex = (curWayPointIndex + 1) % wayPoints.Length;
            targetWayPoint = wayPoints[curWayPointIndex];

            // ���� ������Ʈ
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
        // �Ÿ��� 0.2 �����̸� ������ ������ ����
        return (point - transform.position).sqrMagnitude < 0.04 * speed;
    }

    private void OnDisable()
    {
        curWayPointIndex = 0;
        targetWayPoint = null;
    }
}

