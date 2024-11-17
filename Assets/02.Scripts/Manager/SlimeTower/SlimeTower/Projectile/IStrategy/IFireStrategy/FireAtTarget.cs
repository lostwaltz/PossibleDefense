using System.Collections;
using UnityEngine;

public class FireAtTarget : IFireStrategy
{
    float speed = 5f; // 이동 속도

    public IEnumerator Execute(Transform projectile, Transform targetPos)
    {
        Debug.Log("발사");


        while (projectile != null && Vector3.Distance(projectile.position, targetPos.position) > 0.1f)
        {
            projectile.position = Vector3.MoveTowards(projectile.position, targetPos.position, speed * Time.deltaTime);
            yield return new WaitForSeconds(0.02f); // 대기 시간 조정
        }

        Debug.Log("목표 지점에 도달");
    }

}
