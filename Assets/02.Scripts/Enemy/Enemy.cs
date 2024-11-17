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
    [field: SerializeField] public EnemySO EnemyData { get; private set; }
    private EnemyMovement movement;
    private EnemyHealth health;

    //부활할때마다 바뀔 것들
    private Animator anim;
    //테스트용 퍼블릭
    public Transform[] wayPoints;
    private GameObject model;

    //애니메이션 해쉬, 추후에 타워애니메이션의 데이터가 있으면 합치기
    private string onHitAnimationName = "OnHit";
    private string onDieAnimationName = "OnDie";
    private int OnHitAnimationHash;
    private int OnDieAnimationHash;
    private float DisappearAfterDie = 1f;   //죽고나서 사라지기까지의 시간


    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);
    }

    private void Start()
    {
        //테스트
        Initialize(wayPoints, EnemyType.Slime, wayPoints[1].position);
    }


    public void Initialize(Transform[] waypoints, EnemyType enemyType, Vector3 position)
    {
        //해당 타입의 오브젝트를 자식으로두고 해당 위치에 오브젝트풀에서 받기
        anim = GetComponentInChildren<Animator>();  //해당 오브젝트의 animator 가져오기
        SetModel(enemyType);
        movement.SetUp(waypoints, EnemyData);
        health.SetUp(EnemyData);

        //해당 타입의 프리팹을 자식프리팹으로
    }

    public void SetModel(EnemyType enemyType)
    {
        //해당 타입에 맞는 모델을 자식오브젝트로 => 오브젝트 풀?
    }

    public void TakeDamage(float damage)
    {
        //죽으면 true를 반환
        if (health.TakeDamage(damage))
        {
            OnDie();
            Invoke(nameof(Disappear), DisappearAfterDie);
        }
        else
        {
            OnHit();
            //파티클재생
        }
    }

    public void OnHit()
    {
        anim.SetTrigger(OnHitAnimationHash);
    }

    public void OnDie()
    {
        anim.SetBool(OnDieAnimationHash, true);

        //적 죽음 이벤트 발생
        //적 죽었을 때의 이펙트
    }

    private void Disappear()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        //오브젝트 풀로 다시 변경
    }
}
