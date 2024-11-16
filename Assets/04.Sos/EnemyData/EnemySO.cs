using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultEnemyData", menuName = "Enemy/DefaultEnemy")]
public class EnemySO : ScriptableObject
{
    public string EnemyName {  get; private set; }
    public float baseMaxHP {  get; private set; }
    public float baseDamage {  get; private set; }  //혹시모를 공격?
    public float baseSpeed { get; private set; }
    public float maxHPModifier { get; private set; } = 1f;
    public float speedModifier { get; private set; } = 1f;

    public void ChangeHPModifier(float amount)
    {
        maxHPModifier *= amount;
    }

    public void ChangeSpeedModifier(float amount)
    {
        speedModifier *= amount;
    }
}
