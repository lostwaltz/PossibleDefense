using System.Collections.Generic;

/*Stage*/
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
/*!Stage*/