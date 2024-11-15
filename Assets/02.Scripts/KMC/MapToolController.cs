using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapToolController : MonoBehaviour
{
    public TileMapData mapData;
    public string fileName;

    private void Awake()
    {
        mapData.wayPoints.Clear();
        mapData.towerPoints.Clear();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (!Physics.Raycast(ray, out RaycastHit hit, 1000f)) return;
            
            Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();

            if (renderer == null || renderer.material.color == Color.green) return;
            
            mapData.wayPoints.Add(hit.transform.position + new Vector3(0f, 0.5f, 0f));
            renderer.material.color = Color.green;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (!Physics.Raycast(ray, out RaycastHit hit, 1000f)) return;
            
            Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();

            if (renderer == null || renderer.material.color == Color.blue) return;
            
            mapData.towerPoints.Add(hit.transform.position + new Vector3(0f, 0.5f, 0f));
            renderer.material.color = Color.blue;
        }
        
    }

    public void SaveMapData()
    {
        var soInstance = ScriptableObject.CreateInstance<TileMapData>();

        soInstance.wayPoints = mapData.wayPoints;
        soInstance.towerPoints = mapData.towerPoints;
        
        // 에셋 파일로 저장
        string assetPath = "Assets/02.Scripts/KMC/Data/" + fileName + ".asset";
        AssetDatabase.CreateAsset(soInstance, assetPath);
        AssetDatabase.SaveAssets();
    }
}
