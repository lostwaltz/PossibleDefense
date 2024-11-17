using System.Collections;
using UnityEngine;

public interface IFireStrategy
{
    IEnumerator  Execute(Transform projectilePos, Transform targetPos);
}


//타워 데이터에 맞는 공격 로직 수행
