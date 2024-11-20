using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerChanceData _towerChanceData;
    [SerializeField] private Transform[] tile;
    [SerializeField] private Button spawnButton;
    
    private Dictionary<TowerGrade, GameObject[]> towerPrefabsDictionary = new Dictionary<TowerGrade, GameObject[]>();
    private List<float> _chanceList;
    

    private void Awake()
    {
        SetTowerPrefabs();
        NormalizeTowerProbabilities();
    }


    private void SetTowerPrefabs()
    {
        var grades = Enum.GetValues(typeof(TowerGrade));

        foreach (TowerGrade grade in grades)
        {
            GameObject[] gradePrefabs = Resources.LoadAll<GameObject>($"Towers/{grade}");

            if (gradePrefabs.Length == 0)
            {
                Debug.LogWarning($"해당 등급에 타워 프리팹이 없습니다: {grade}");
                continue;
            }

            towerPrefabsDictionary[grade] = gradePrefabs;
        }
    }

    public GameObject SpawnTowerByProbability()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeChance = 0f;

        for (int i = 0; i < _chanceList.Count; i++)
        {
            cumulativeChance += _chanceList[i];
            if (randomValue <= cumulativeChance)
            {
                return InstantiateTowerByGrade(i);
            }
        }

        return null;
    }

    private GameObject InstantiateTowerByGrade(int gradeIndex)
    {
        TowerGrade grade = (TowerGrade)gradeIndex;

        if (!towerPrefabsDictionary.TryGetValue(grade, out GameObject[] gradePrefabs) || gradePrefabs.Length == 0)
        {
            Debug.LogWarning($"해당 등급에 타워 프리팹이 없습니다: {grade}");
            return null;
        }
        
        int randomIndex = Random.Range(0, gradePrefabs.Length);

        GameObject selectedPrefab = gradePrefabs[randomIndex];

        if (selectedPrefab == null)
        {
            Debug.LogWarning($"선택된 타워 프리팹이 null입니다: Grade={grade}, PrefabIndex={randomIndex}");
            return null;
        }

        return Instantiate(selectedPrefab);
    }
    
    private void NormalizeTowerProbabilities()
    {
        float totalProbability = 0f;
        _chanceList = _towerChanceData.GetChanceList();

        foreach (var chance in _chanceList)
        {
            totalProbability += chance;
        }

        if (totalProbability > 1f)
        {
            for (int i = 0; i < _chanceList.Count; i++)
            {
                _chanceList[i] /= totalProbability;
            }
        }
    }
}