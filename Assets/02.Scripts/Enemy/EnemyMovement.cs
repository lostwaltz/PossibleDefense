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

        // �ʱ� ��������Ʈ ����
        curWayPointIndex = 0;
        targetWayPoint = wayPoints[curWayPointIndex];
        UpdateDirection();

        //�������� �̺�Ʈ�� OnDead�Լ� ����ؼ� ���Ͱ� ������ �������� �ʵ���
        //��� ���Ͱ� �ѹ��� ���ߴ°� �����ϱ����� ���׸����� EnemyMovement���Ͽ� �μ��� this��?
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDead()
    {
        isDead = true;
    }


    private bool IsCloseToPoint(Vector3 point)
    {
        // �Ÿ��� 0.2 �����̸� ������ ������ ����
        return (point - transform.position).sqrMagnitude < 1f;
    }

    private void OnDisable()
    {
        curWayPointIndex = 0;
        targetWayPoint = null;
        isDead = false;
        //�̺�Ʈ ����Ѱ� �������ֱ�
    }
}

