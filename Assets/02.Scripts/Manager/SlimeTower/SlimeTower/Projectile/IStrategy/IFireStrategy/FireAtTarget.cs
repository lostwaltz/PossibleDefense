using System.Collections;
using UnityEngine;

public class FireAtTarget : IFireStrategy
{
    public void Execute(Transform projectile, Transform targetPos , float speed = 5f)
    {
        projectile.position = Vector3.MoveTowards(projectile.position, targetPos.position, speed * Time.deltaTime);
    }
}