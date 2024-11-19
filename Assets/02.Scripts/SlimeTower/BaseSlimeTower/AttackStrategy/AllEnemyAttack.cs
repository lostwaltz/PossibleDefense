using UnityEngine;

public class AllEnemyAttack : IAttackStrategy
{
    public Vector3 boxSize = new Vector3(1000, 100, 1000);
    private int _targetLayerMask = LayerMask.GetMask("Enemy");
    private Collider[] _results = new Collider[1000000];
    private float _damage;
    private Transform _firePos;


    //고민을 좀 해봐야함 
    public void Execute(Transform target)
    {
        int detectedCount = Physics.OverlapBoxNonAlloc(
            _firePos.position,
            boxSize,
            _results,
            Quaternion.identity,
            _targetLayerMask,
            QueryTriggerInteraction.Collide
        );

        for (int i = 0; i < detectedCount; i++)
        {
            Collider collider = _results[i];
            if ((1 << collider.gameObject.layer & _targetLayerMask) != 0)
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(_damage);
                }
            }
        }
    }


    public void Setting(Transform firePos, Transform targetPos, float damage)
    {
        _damage = damage;
        _firePos = firePos;
    }
}