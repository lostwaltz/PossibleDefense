using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Grade
{
    Common,
    Uncommon
}

public class CharSpawner : MonoBehaviour
{
    [SerializeField]private GameObject[] SlimePrefabs;
    private Vector3 spawnPos;

    private float commonChance = 10f;
    private float uncommonChance = 30f;

    private Camera camera;
    private Ray ray;
    RaycastHit hit;

    int layerMask;

    private Coroutine _coroutine;

    private void Awake()
    {
        camera = Camera.main;
        CalculateGradeChance();
        layerMask = LayerMask.GetMask("Water");
    }

    private void CalculateGradeChance()
    {
        float totalChance = commonChance + uncommonChance;
        commonChance = commonChance / totalChance;
        uncommonChance = uncommonChance / totalChance;
    }

    

    public void TrySpawnEnemy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(GetSpawnPointByMousePointLay());
    }

    private IEnumerator GetSpawnPointByMousePointLay()
    {
        while (true)
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    spawnPos = hit.point;
                    SpawnSlimeByGradeChance();
                }
            }

            yield return null;
        }
    }

    // private void OnDrawGizmos()
    // {
    //     if (camera != null)
    //     {
    //         ray = camera.ScreenPointToRay(Input.mousePosition);
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawRay(ray.origin, ray.direction * 100);
    //     }
    // }

    public void SpawnSlimeByGradeChance()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        float chance = Random.Range(0f, 1f);
        if (chance <= commonChance)
        {
            Instantiate(SlimePrefabs[0],spawnPos,Quaternion.identity);
        }
        else if (chance <= commonChance + uncommonChance)
        {
            Instantiate(SlimePrefabs[1],spawnPos,Quaternion.identity);
        }

        spawnPos = Vector3.zero;
    }
}