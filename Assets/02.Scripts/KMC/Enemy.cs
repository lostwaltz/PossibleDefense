using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TileMapData mapData;
    
    private int _currentWaypointIndex = 0;

    private void Start()
    {
        transform.position = mapData.wayPoints[0];
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, mapData.wayPoints[_currentWaypointIndex]) < 0.1f && _currentWaypointIndex < mapData.wayPoints.Count - 1)
            _currentWaypointIndex++;
        
        transform.position = Vector3.MoveTowards(transform.position, mapData.wayPoints[_currentWaypointIndex], 2f * Time.deltaTime);
    }
}
