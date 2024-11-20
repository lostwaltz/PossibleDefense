using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPoolSetUpExample : MonoBehaviour
{
    public BaseProjectile projectile;
    
    
    private void Awake()
    {
        // 방법 1. 리소스 로드를 통해 동적 로드 후 넘겨주기
        BaseProjectile baseProjectileResource = Resources.Load<BaseProjectile>("ResourcePath");
        // 키값을 통해 새로운 풀 생성
        ObjectPoolManager.Instance.CreateNewPool("Example1", baseProjectileResource, 10, 10); 
        //  키값을 통해 접근 하면 됨.
        BaseProjectile example1Object = ObjectPoolManager.Instance.GetPooledObject<BaseProjectile>("Example1");
        
        
        // 방법 2. 그냥 게임 오브젝트 인스펙터로 변수 바인딩해서 넘겨주기
        ObjectPoolManager.Instance.CreateNewPool("Example2", Instantiate(projectile), 10, 10);
        BaseProjectile example2Object = ObjectPoolManager.Instance.GetPooledObject<BaseProjectile>("Example2");
    }


    
    
    
}
