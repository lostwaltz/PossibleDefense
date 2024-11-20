
using JetBrains.Annotations;
using System.Collections.Generic;

public static class StageConstain
{
    public static readonly string EnemyTile = "EnemyTile";
    public static readonly string PlayerTile = "PlayerTile";
    public static readonly string EnemyMoveStartTile = "EnemyMoveStartTile";
    public static readonly string EnemyMoveEndTile = "EnemyMoveEndTile";

    public static readonly string MapMatrixDBPath = "StageDB/MapMatrixDB/StageMatrix";
    public static readonly string StageWayPointDBPath = "StageDB/StageWayPointDB/StageWayPoint";
    public static readonly string StageWaveStageDBPath = "StageDB/WaveStageDB/WaveStage";

}

public static class StageUpgradeConstain
{
    public static readonly string MaxUpgrade = "MAX";
}

public enum StageTileTag
{
    EnemyWayTile,
    TowerTile,
    SpawnTile,
    EndTile
}

[System.Serializable]
public struct EnemySpawnData
{
    public int EnemyCount;
    public float EnemySpawnTimer;
}

[System.Serializable]
public struct WaveStageData
{
    public int WaveNum;
    public float WaveTime;
    public Dictionary<int, EnemySpawnData> WaveSpawnData; // key : Enemy Type , value : 해당 Wave에 등장할 적의 갯수와 등장 쿨타임
}

