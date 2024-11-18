
public static class StageConstain
{
    public static readonly string EnemyTile = "EnemyTile";
    public static readonly string PlayerTile = "PlayerTile";
    public static readonly string EnemyMoveStartTile = "EnemyMoveStartTile";
    public static readonly string EnemyMoveEndTile = "EnemyMoveEndTile";

    public static readonly string MapMatrixDBPath = "StageDB/MapMatrixDB/StageMatrix";
    public static readonly string StageWayPointDBPath = "StageDB/StageWayPointDB/StageWayPoint";
    
}

public enum StageTileTag
{
    EnemyTile,
    PlayerTile, 
    EnemyMoveStartTile, 
    EnemyMoveEndTile
}

