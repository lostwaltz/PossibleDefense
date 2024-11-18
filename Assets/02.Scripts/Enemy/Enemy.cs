using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage);
}

public class Enemy : MonoBehaviour, IDamagable
{
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

    public void TakeDamage(float damage)
    {
        int result = health.TakeDamage(damage);
        if (result > 0)                                      
        {
            //1 damaged
            OnHit();        
        }
        else if(result < 0)                                  
        {
            //-1 only damaged shield (nothing to do)
            return;
        }
        else                                                 
        {
            //0 dead
            OnDie();
            Invoke(nameof(ReturnToPool), DisappearAfterDie);
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
    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
