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

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

        OnHitAnimationHash = Animator.StringToHash(onHitAnimationName);
        OnDieAnimationHash = Animator.StringToHash(onDieAnimationName);
    }

    public void OnHit()
    {
        anim.SetTrigger(OnHitAnimationHash);
    }

    public void OnDie()
    {
        anim.SetBool(OnDieAnimationHash, true);
    }

}
