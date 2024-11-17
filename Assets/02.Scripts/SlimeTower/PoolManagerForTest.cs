using System;
using UnityEngine;

public class PoolManagerForTest: Singleton<PoolManagerForTest>
{
    public  ObjectPool Pool;

    private void Start()
    {
        Pool = GetComponent<ObjectPool>();
    }
}