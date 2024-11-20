using UnityEngine;

//TODO 타워의 스탯과 연관있이 할지 없지를 결정 해줘야 함.
public class DamageParticle : ExecuteParticle
{
    [SerializeField] private int damageRange;

    private float _damage;
    private Collider[] _results = new Collider[50]; //상수 ? 사용 
    private int _targetLayerMask;

    protected override void Awake()
    {
        base.Awake();
        _targetLayerMask = LayerMask.GetMask("Enemy");
    }


    public void Setting(Transform startPos, float damage)
    {
        transform.position = new Vector3(startPos.position.x, 1.5f, startPos.position.z);
        _damage = damage;
        Debug.Log(damage);
    }

    public override void Execute()
    {
        if(IsExecute)
            return;

        IsExecute = true;
        
        Debug.Log("파티클 데미지");

        int count = Physics.OverlapSphereNonAlloc(transform.position,
            damageRange, _results);

        if (count <= 0) return;

        for (int i = 0; i < count; i++)
        {
            var collider = _results[i];
            if ((_targetLayerMask & (1 << collider.gameObject.layer)) == 0) continue;

            collider.GetComponent<EnemyHealth>().TakeDamage(_damage);
        }
    }


    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = new Color(0, 1, 0, 0.5f); 
    //     Gizmos.DrawSphere(transform.position, damageRange);
    // }
}