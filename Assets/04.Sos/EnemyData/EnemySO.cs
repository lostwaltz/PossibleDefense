using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemyData", menuName = "Enemy/DefaultEnemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public float baseMaxHP {  get; private set; }
    [field: SerializeField] public float baseDamage { get; private set; }  // not sure
    [field: SerializeField] public float baseSpeed { get; private set; }
    [field: SerializeField] public float evasion { get; private set; } 
    [field: SerializeField] public float shield { get; private set; }
    [field: SerializeField] public float maxHPModifier { get; private set; } = 1f;
    [field: SerializeField] public float speedModifier { get; private set; } = 1f;

    public void ChangeHPModifier(float amount)
    {
        maxHPModifier *= amount;
    }

    public void ChangeSpeedModifier(float amount)
    {
        speedModifier *= amount;
    }
}
