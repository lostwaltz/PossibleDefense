 
using System;
using UnityEngine;

public class TowerAttackRangeIndicator : MonoBehaviour
{
    private void Awake()
    {
        OffAttackRangeIndicator();
    }

    public void OnAttackRangeIndicator(Transform pos, float range)
    {
        gameObject.SetActive(true);

        float attackRange = range * 2.0f; 
        transform.localScale = Vector3.one * attackRange;

        Vector3 newPosition = pos.position;
        newPosition.y += 1.6f;  
        transform.position = newPosition;
    }

    public void OffAttackRangeIndicator()
    {
        gameObject.SetActive(false);
    }

}
