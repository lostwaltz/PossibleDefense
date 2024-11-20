using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManagerForTest: Singleton<PoolManagerForTest>
{
    [FormerlySerializedAs("Pool")] public  ObjectPoolLegacy poolLegacy;

    private void Start()
    {
        poolLegacy = GetComponent<ObjectPoolLegacy>();
    }
}