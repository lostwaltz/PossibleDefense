using UnityEngine;

public class BasicHitStrategy : IHitStrategy
{
    private GameObject target;

    public BasicHitStrategy( float damage)
    {
    }

    public void Execute()
    {
        Debug.Log("단일 대상 데미지");
    }
}