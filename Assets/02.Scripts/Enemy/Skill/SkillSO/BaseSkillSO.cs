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
    public virtual void SoundPlay(string id, Vector3 position)
    {
        SoundManager.Instance.PlayClip(id, position);
    }
}
