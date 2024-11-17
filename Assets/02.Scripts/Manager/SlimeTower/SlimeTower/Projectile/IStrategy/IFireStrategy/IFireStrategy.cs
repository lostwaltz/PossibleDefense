using System.Collections;
using UnityEngine;

public interface IFireStrategy
{ 
    void Execute(Transform projectilePos, Transform targetPos, float speed = 5f);
}


 