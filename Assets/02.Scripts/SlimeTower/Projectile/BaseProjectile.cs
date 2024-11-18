using System;
using System.Collections;
using UnityEngine;


public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private IHitStrategy _hitStrategy;
    private IFireStrategy _fireStrategy;
    private Transform _target;
    private float _damage;
    private int _enemyLayer;


    private void Start()
    {
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }


    private void Update()
    {
        if (_fireStrategy != null)
            _fireStrategy.Execute(transform, _target, moveSpeed);


        if (!_target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }


    public void SetProjectile(IFireStrategy fireStrategy, IHitStrategy hitStrategy, Transform target, float damage)
    {
        _fireStrategy = fireStrategy;
        _hitStrategy = hitStrategy;

        _target = target;
        _damage = damage;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enemyLayer)
        {
            _hitStrategy.Execute();
            other.GetComponent<EnemyHealth>().TakeDamage(_damage);
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        _hitStrategy = null;
        _fireStrategy = null;
    }
}