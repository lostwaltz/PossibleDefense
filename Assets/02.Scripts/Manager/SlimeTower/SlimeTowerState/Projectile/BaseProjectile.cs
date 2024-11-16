using System;
using UnityEngine;



public class BaseProjectile: MonoBehaviour
{
    
    private IHitStrategy hitStrategy;
    private int enemyLayer;

    private void Start()
    {
        hitStrategy = new BasicHitStrategy(5);
        enemyLayer =  LayerMask.NameToLayer("Enemy");
    }

    public  void SetHitStrategy( IHitStrategy _hitStrategy)
    {
        hitStrategy = _hitStrategy;
    }
    
    
    private void OnTriggerEnter(Collider other)
    { 

        if (other.gameObject.layer == enemyLayer)
        {
            hitStrategy.Execute();
            gameObject.SetActive(false);
        }
    }

}