 using UnityEngine;

 
 
public abstract class AttackStrategySO : ScriptableObject
{
    public abstract IAttackStrategy GetAttackStrategy();
}