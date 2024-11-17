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
    [field: SerializeField] private Canvas HPCanvas;
    private EnemyMovement movement;
    private EnemyHealth health;
    private Camera cam;

    //��Ȱ�Ҷ����� �ٲ� �͵�
    private Animator anim;
    //�׽�Ʈ�� �ۺ�
    public Transform[] wayPoints;
    public GameObject model;

    //�ִϸ��̼� �ؽ�, ���Ŀ� Ÿ���ִϸ��̼��� �����Ͱ� ������ ��ġ��
    private string onHitAnimationName = "OnHit";
    private string onDieAnimationName = "OnDie";
    private int OnHitAnimationHash;
    private int OnDieAnimationHash;
    private float DisappearAfterDie = 1f;   //�װ��� ������������ �ð�


    private void Awake()
    {
        cam = Camera.main;
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);
    }


    public void Initialize(Transform[] waypoints, EnemyType enemyType)
    {
        anim = GetComponentInChildren<Animator>();
        SetModel(enemyType);
        health.SetUp(EnemyData);
        movement.SetUp(waypoints, EnemyData, model);

        HPCanvas.transform.rotation = Quaternion.LookRotation(cam.transform.position);

        //�ش� Ÿ���� �������� �ڽ�����������?
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
        if (anim == null)
        {
            Debug.Log("animator null");
            return;
        }

        anim.SetBool(OnDieAnimationHash, true);
        movement.OnDead();

        //�� ���� �̺�Ʈ �߻�
        //�� �׾��� ���� ����Ʈ
    }

    private void Disappear()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        //������Ʈ Ǯ�� �ٽ� ��ȯ
        gameObject.SetActive(false);
    }
}
