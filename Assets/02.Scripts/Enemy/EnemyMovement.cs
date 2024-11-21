using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform model;

    public float rotationSpeed = 10f;
    private Vector3[] wayPoints;
    private int curWayPointIndex = 0;
    private Vector3 dir;
    private Vector3 targetWayPoint;

    private bool isDead = false;

    public ForceReceiver forceReceiver;
    private float speed => forceReceiver.GetSpeed();

    private void Awake()
    {
        forceReceiver = GetComponent<ForceReceiver>();
    }

    public void SetUp(Vector3[] waypoints, EnemySO data)
    {
        this.wayPoints = waypoints;
        //this.model = data.modelPrefab;
        forceReceiver.Initialize(data.baseSpeed * data.speedModifier);

        curWayPointIndex = 0;
        targetWayPoint = wayPoints[curWayPointIndex];
        UpdateDirection();

        isDead = false;
    }

    private void UpdateDirection()
    {
        if (targetWayPoint != null)
        {
            dir = (targetWayPoint - transform.position).normalized;
        }
    }

    public void FixedUpdate()
    {
        if (!isDead)
            Move();
    }

    public void Move()
    {
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        transform.Translate(Time.deltaTime * speed * dir, Space.World);

        if (IsCloseToPoint(targetWayPoint))
        {
            curWayPointIndex = (curWayPointIndex + 1) % wayPoints.Length;
            targetWayPoint = wayPoints[curWayPointIndex];

            UpdateDirection();
        }

        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            model.transform.rotation =
                Quaternion.Slerp(model.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void OnDead()
    {
        isDead = true;
    }

    private bool IsCloseToPoint(Vector3 point)
    {
        return (point - transform.position).sqrMagnitude < 0.04 * speed;
    }

    private void OnDisable()
    {
        curWayPointIndex = 0;
        targetWayPoint = Vector3.zero;
    }
}