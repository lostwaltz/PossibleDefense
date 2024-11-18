using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResultOfDamage
{
    HealthReduced,
    OnlyShield,
    Evasion,
    Dead
}

public interface IDamagable
{
    void TakeDamage(float damage);
}

public class Enemy : MonoBehaviour, IDamagable
{
    private EnemyMovement movement;
    private EnemyHealth health;

    private Animator anim;
    //for test
    public Vector3[] wayPoints;
    public GameObject model;

    private string onHitAnimationName = "OnHit";
    private string onDieAnimationName = "OnDie";
    private int OnHitAnimationHash;
    private int OnDieAnimationHash;
    private float DisappearAfterDie = 1f;   


    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<Animator>();

        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);
    }

    //not require id, but SO
    public void Initialize(Vector3[] waypoints, EnemySO enemySO)
    {
        this.wayPoints = waypoints;
        health.SetUp(enemySO);
        movement.SetUp(waypoints, enemySO, model);
    }

    public void TakeDamage(float damage)
    {
        ResultOfDamage result = health.TakeDamage(damage);

        switch (result)
        {
            case ResultOfDamage.Evasion:
            case ResultOfDamage.OnlyShield:
                break;

            case ResultOfDamage.HealthReduced:
                OnHit();
                break;

            case ResultOfDamage.Dead:
                OnDie();
                Invoke(nameof(ReturnToPool), DisappearAfterDie);
                break;
        }
    }

    public void OnHit()
    {
        anim.SetTrigger(OnHitAnimationHash);
    }

    public void OnDie()
    {
        anim.SetBool(OnDieAnimationHash, true);
        movement.OnDead();
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
