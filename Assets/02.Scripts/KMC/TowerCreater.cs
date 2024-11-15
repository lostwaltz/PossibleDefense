using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TowerCreater : MonoBehaviour
{
    public TileMapData mapData;
    public GameObject towerPrefab;

    private List<Vector3> _towerPositions;

    private void Awake()
    {
        _towerPositions = new List<Vector3>(mapData.towerPoints);
    }

    public void CreateTower()
    {
        if(_towerPositions.Count <= 0)
            return;
        
        var index = Random.Range(0, _towerPositions.Count);
        
        GameObject go = Instantiate(towerPrefab, _towerPositions[index], Quaternion.identity);
        _towerPositions.RemoveAt(index);
    }
}
