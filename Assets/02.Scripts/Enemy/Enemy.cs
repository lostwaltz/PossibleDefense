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

    //��Ȱ�Ҷ����� �ٲ� �͵�
    private Animator anim;
    //�׽�Ʈ�� �ۺ�
    public Transform[] wayPoints;
    private GameObject model;

    //�ִϸ��̼� �ؽ�, ���Ŀ� Ÿ���ִϸ��̼��� �����Ͱ� ������ ��ġ��
    private string onHitAnimationName = "OnHit";
    private string onDieAnimationName = "OnDie";
    private int OnHitAnimationHash;
    private int OnDieAnimationHash;
    private float DisappearAfterDie = 1f;   //�װ��� ������������ �ð�


    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);
    }

    private void Start()
    {
        //�׽�Ʈ
        Initialize(wayPoints, EnemyType.Slime, wayPoints[1].position);
    }


    public void Initialize(Transform[] waypoints, EnemyType enemyType, Vector3 position)
    {
        //�ش� Ÿ���� ������Ʈ�� �ڽ����εΰ� �ش� ��ġ�� ������ƮǮ���� �ޱ�
        anim = GetComponentInChildren<Animator>();  //�ش� ������Ʈ�� animator ��������
        SetModel(enemyType);
        movement.SetUp(waypoints, EnemyData);
        health.SetUp(EnemyData);

        //�ش� Ÿ���� �������� �ڽ�����������
    }

    public void SetModel(EnemyType enemyType)
    {
        //�ش� Ÿ�Կ� �´� ���� �ڽĿ�����Ʈ�� => ������Ʈ Ǯ?
    }

    public void TakeDamage(float damage)
    {
        //������ true�� ��ȯ
        if (health.TakeDamage(damage))
        {
            OnDie();
            Invoke(nameof(Disappear), DisappearAfterDie);
        }
        else
        {
            OnHit();
            //��ƼŬ���
        }
    }

    public void OnHit()
    {
        anim.SetTrigger(OnHitAnimationHash);
    }

    public void OnDie()
    {
        anim.SetBool(OnDieAnimationHash, true);

        //�� ���� �̺�Ʈ �߻�
        //�� �׾��� ���� ����Ʈ
    }

    private void Disappear()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        //������Ʈ Ǯ�� �ٽ� ����
    }
}
