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

        // ���� Ÿ�� ��������Ʈ ��������
        Transform targetWayPoint = wayPoints[curWayPointIndex];

        // ���� ���
        dir = (targetWayPoint.position - transform.position).normalized;

        // �̵�
        transform.Translate(Time.deltaTime * speed * dir, Space.World);

        // ��������Ʈ ���� üũ
        if ((targetWayPoint.position - transform.position).sqrMagnitude < 0.04f)
        {
            // ���� ��������Ʈ�� ��ȯ
            curWayPointIndex = (curWayPointIndex + 1) % wayPoints.Length;

            // ȸ�� ���� (�ٶ󺸴� �������� �ٷ� ����)
            Vector3 lookDirection = wayPoints[curWayPointIndex].position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDisable()
    {
        curWayPointIndex = -1;
    }
}
