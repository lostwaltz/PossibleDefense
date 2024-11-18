using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //for test
    public Vector3[] wayPoints;

    private float DisappearAfterDie = 1f;   

    private void Awake()
    {
        anim = GetComponent<EnemyAnimationController>();
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        health.SetUp(enemyData);

        health.OnDamage += Damage;
        health.OnDead += Die;
    }

    private void Die()
    {
        anim.OnDie();
        movement.OnDead();

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
    }

    public virtual void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
