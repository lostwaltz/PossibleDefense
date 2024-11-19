using UnityEngine;

public class SplashHitStrategy : IHitStrategy
{
    private Transform _projectilePos;
    private float _damage;
    private Collider[] colls = new Collider[10];
    private int _targetLayerMask = LayerMask.GetMask("Enemy");


    public SplashHitStrategy(float damage, Transform projectilePos)
    {
        _projectilePos = projectilePos;
        _damage = damage;
    }


    public void Execute()
    {
        
        //TODO: 파티클이 생긴다면 파티클에 COLIIDER를 달고 거기에 부딪히면 데미지 주는 방식으로 변경! 
        int count = Physics.OverlapSphereNonAlloc(_projectilePos.position, 5f, colls);

        for (int i = 0; i < count; i++)
        {
            var collider = colls[i];

            if ((_targetLayerMask & (1 << collider.gameObject.layer)) == 0) continue;

            var enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

 
}