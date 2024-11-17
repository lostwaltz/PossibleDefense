using UnityEngine;


//TODO 오브젝트 풀에서 꺼내 오기 
public class SingleTargetAttackByProjectile : IAttackStrategy
{
    private IFireStrategy fireStrategy = new FireAtTarget();
    private IHitStrategy _hitStrategy;

    private Transform targetPos;
    private Transform towerTransform;

    
    
    //Transform targetPos 이 부분은 SingleTargetAttackByProjectile여기서 감지 하도록 변경
    //targetPos대신 감지 범위를 가져와서 감지 범위에서 감지 후 해당 target을 향해 발사 하도록 수정 
    
    
    public SingleTargetAttackByProjectile(Transform towerTransform , Transform targetPos)
    {
        this.targetPos = targetPos;
        this.towerTransform = towerTransform;
    }
    
    
    public void Execute()
    {
        //오브젝트풀로부터 projectile을 받아옴
        GameObject projectile = PoolManagerForTest.Instance.Pool.SpawnFromPool("Bullet");
        projectile.transform.position = towerTransform.position;
        CoroutineRunner.Instance.StartCoroutine(fireStrategy.Execute(projectile.transform,targetPos));
    }
}