using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float speed;

    private int wayPointIndex = 0;
    private float distanceThreshold = 0.05f;
    private Transform[] wayPoints;

    public event Action<Monster> Die;

    public void SetWayPoints(Transform[] _wayPoints)
    {
        wayPoints = _wayPoints;
        StartCoroutine(MoveToWayPoint());
    }

    private IEnumerator MoveToWayPoint()
    {
        while (true)
        {
            Vector3 direction = (wayPoints[wayPointIndex].position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;

            if (Vector3.Distance(wayPoints[wayPointIndex].position, transform.position) < distanceThreshold)
                UpdateWayPointIndex();


            yield return null;
        }
    }

    private void UpdateWayPointIndex()
    {
        if (wayPointIndex < wayPoints.Length - 1)
            wayPointIndex++;

        else
        {
            wayPointIndex = 0;
        }
    }

    public void TakeDamage()
    {
        Die?.Invoke(this);
        Destroy(gameObject);
    }
}