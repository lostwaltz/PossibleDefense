using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (fileName = "EnemyDatabase", menuName ="Enemy/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    [field: SerializeField] public List<EnemySO> enemyDataList { get; private set; }

    public EnemySO GetEnemy(int id)
    {
        return enemyDataList.FirstOrDefault<EnemySO>(x => x.id == id);
    }
}
