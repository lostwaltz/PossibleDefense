using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator anim;

    private const string onHitAnimationName = "OnHit";
    private const string onDieAnimationName = "OnDie";
    private int OnHitAnimationHash;
    private int OnDieAnimationHash;

    [SerializeField] private const float HitAnimationCooldown = 0.5f;
    private Coroutine HitCooldownCoroutine;
    private WaitForSeconds cooldown;
    private bool canHit = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);

        cooldown = new WaitForSeconds(HitAnimationCooldown);
    }

    private void OnEnable()
    {
        canHit = true;
    }

    public void OnHit()
    {
        if (!canHit) return;
        anim.SetTrigger(OnHitAnimationHash);

        if (HitCooldownCoroutine != null)
        {
            StopCoroutine(HitCooldownCoroutine);
        }
        HitCooldownCoroutine = StartCoroutine(StartCooldown());
    }

    public void OnDie()
    {
        anim.SetBool(OnDieAnimationHash, true);
        canHit = false;
    }

    private IEnumerator StartCooldown()
    {
        canHit = false;
        yield return cooldown;
        canHit = true;
        HitCooldownCoroutine = null;    //코루틴 초기화
    }
}
