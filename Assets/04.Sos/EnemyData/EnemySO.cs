using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemyData", menuName = "Enemy/DefaultEnemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public float baseMaxHP {  get; private set; }
    [field: SerializeField] public float baseDamage {  get; private set; }  //혹시모를 공격?
    [field: SerializeField] public float baseSpeed { get; private set; }
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
