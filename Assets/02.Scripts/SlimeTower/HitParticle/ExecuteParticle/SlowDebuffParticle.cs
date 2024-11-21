using UnityEngine;

public class SlowDebuffParticle : ExecuteParticle
{
    [SerializeField] private int debuffRange;
    [SerializeField] [Range(0, 100)] private float speedReductionPercent;
    private Collider[] _results = new Collider[50]; //상수 ? 사용 
    private int _targetLayerMask;

    protected override void Awake()
    {
        base.Awake();
        _targetLayerMask = LayerMask.GetMask("Enemy");
    }


    public override void Setting(Transform startPos, Vector3 offset)
    {
        transform.position = new Vector3(startPos.position.x, 5f, startPos.position.z);
    }

    public override void Execute()
    {
        if (IsExecute)
            return;

        IsExecute = true;


        int count = Physics.OverlapSphereNonAlloc(transform.position,
            debuffRange, _results);

        if (count <= 0) return;

        for (int i = 0; i < count; i++)
        {
            var collider = _results[i];
            if ((_targetLayerMask & (1 << collider.gameObject.layer)) == 0) continue;

            collider.GetComponent<EnemyMovement>().forceReceiver.SpeedBuff(speedReductionPercent, particleLifetimeDuration, false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, debuffRange);
    }
}