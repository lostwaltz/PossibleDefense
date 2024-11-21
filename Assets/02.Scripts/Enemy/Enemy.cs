using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievement;
public interface IDamagable
{
    void TakeDamage(float damage);
}

public class Enemy : MonoBehaviour, IDamagable
{
    [field: SerializeField] EnemySO enemyData;
    public EnemyMovement movement {  get; private set; }
    public EnemyHealth health { get; private set; }
    public EnemyAnimationController anim { get; private set; }
    private EnemySkillController skillController;
    private bool _hasSkillController = false;

    //for test
    public Vector3[] wayPoints;

    private float DisappearAfterDie = 1f;   

    private void Awake()
    {
        anim = GetComponent<EnemyAnimationController>();
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        _hasSkillController = TryGetComponent<EnemySkillController>(out skillController);
    }

    private void Start()
    {
        health.SetUp(enemyData);

        health.OnDamage -= Damage;
        health.OnDead -= Die;

        health.OnDamage += Damage;
        health.OnDead += Die;
    }

    private void Die()
    {
        anim.OnDie();
        movement.OnDead();
        if (_hasSkillController)
        {
            skillController.OnDead();
        }

        Invoke(nameof(ReturnToPool), DisappearAfterDie);
    }

    private void Damage()
    {
        anim.OnHit();
    }

    //not require id, but SO
    public void Initialize(Vector3[] waypoints, EnemySO enemySO)
    {
        this.wayPoints = waypoints;
        health.SetUp(enemySO);
        movement.SetUp(waypoints, enemySO);
        StageManager.Instance.CurEnemyCount++;
    }

    public virtual void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    private void ReturnToPool()
    {
        StageManager.Instance.CurEnemyCount--;
        EventManager.Instance.Publish(EventManager.Channel.Achievement, new Achievement.EventAchievement(Achievement.Action.Kill, Target.Monster, 1f, 0));
       
        gameObject.SetActive(false);
    }
}
