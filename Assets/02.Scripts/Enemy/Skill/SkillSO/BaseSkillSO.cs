using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = ("BaseSkillSO"), menuName = ("Enemy/Skill/BaseSkillSO"))]
public abstract class BaseSkillSO : ScriptableObject
{
    public float cooldown;
    public float radius;
    public LayerMask TargetLayer;

    public abstract void Execute(Enemy enemy);
}
