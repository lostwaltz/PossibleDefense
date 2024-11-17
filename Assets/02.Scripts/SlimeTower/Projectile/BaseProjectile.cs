using System;
using UnityEngine;


public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private IHitStrategy _hitStrategy;
    private IFireStrategy _fireStrategy;
    private Transform _target;

    private int _enemyLayer;

 
    private void Start()
    {
         _enemyLayer = LayerMask.NameToLayer("Enemy");
    }
    
    
    private void Update()
    {
         if (_fireStrategy != null)
            _fireStrategy.Execute(transform, _target);
    }


    public void SetProjectile(IFireStrategy fireStrategy, IHitStrategy hitStrategy, Transform target)
    {
        _fireStrategy = fireStrategy;
        _hitStrategy = hitStrategy;
        _target = target;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _enemyLayer)
        {
            _hitStrategy.Execute();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _hitStrategy = null;
        _fireStrategy = null;
    }
}