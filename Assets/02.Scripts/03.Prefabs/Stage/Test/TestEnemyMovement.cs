using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestEnemyMovement : MonoBehaviour
{
    public List<Vector3> curEnmeyWayPointData = new List<Vector3>();
    public int curIndex = 0;
    public float speed = 5.0f;

    public void Initialize(Vector3[] WayPointData)
    {
        curEnmeyWayPointData = WayPointData.ToList<Vector3>();
    }

    public void OperateEnemy()
    {
        StartCoroutine(MoveWayPoint());
    }

    IEnumerator MoveWayPoint()
    {
        while(true)
        {
            if (curIndex >= curEnmeyWayPointData.Count) curIndex = 0;
 
            if (Vector3.Distance(curEnmeyWayPointData[curIndex], transform.position) <= 0.1f)
            {
                curIndex++;
            }
            else
            {
                Vector3 dir = (curEnmeyWayPointData[curIndex] - transform.position).normalized;

                transform.position += dir * Time.deltaTime * speed;
            }

            yield return null;
        }

        
    }
}
