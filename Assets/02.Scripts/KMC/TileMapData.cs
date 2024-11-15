using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PossibleDefense/TileMapData", fileName = "TileMapData")]
public class TileMapData : ScriptableObject
{
    public List<Vector3> wayPoints = new();
    public List<Vector3> towerPoints = new();
}
