using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Slime,
    Rabit,
    MetalHelmet,
    Viking,
    King
}

public interface IDamagable
{
    void TakeDamage(float damage);
}

public class Enemy : MonoBehaviour, IDamagable
{
    //this data required?
    [field: SerializeField] public EnemySO EnemyData { get; private set; }
    [field: SerializeField] private Canvas HPCanvas;
    private EnemyMovement movement;
    private EnemyHealth health;
    private Camera cam;

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
        cam = Camera.main;
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);
    }

    //not require id, but SO
    public void Initialize(Vector3[] waypoints, EnemySO enemySO)
    {
        this.wayPoints = waypoints;
        anim = GetComponentInChildren<Animator>();
        health.SetUp(enemySO);
        movement.SetUp(waypoints, enemySO, model);

        HPCanvas.transform.rotation = Quaternion.LookRotation(cam.transform.position);
    }



    public void Initialize(Vector3[] waypoints, EnemyType enemyType)
    {
        this.wayPoints = waypoints;
        anim = GetComponentInChildren<Animator>();
        SetModel(enemyType);
        health.SetUp(EnemyData);
        movement.SetUp(waypoints, EnemyData, model);

        HPCanvas.transform.rotation = Quaternion.LookRotation(cam.transform.position);

    }

    public void SetModel(EnemyType enemyType)
    {
        //don't need anymore
    }

    public void TakeDamage(float damage)
    {
        if (health.TakeDamage(damage))
        {
            OnDie();
            Invoke(nameof(Disappear), DisappearAfterDie);
        }
        else
        {
            OnHit();
        }
    }

    public void OnHit()
    {
        anim.SetTrigger(OnHitAnimationHash);
    }

    public void OnDie()
    {
        if (anim == null)
        {
            Debug.Log("animator null");
            return;
        }

        anim.SetBool(OnDieAnimationHash, true);
        movement.OnDead();
    }

    private void Disappear()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
