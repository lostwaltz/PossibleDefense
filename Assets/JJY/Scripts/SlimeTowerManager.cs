using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTowerManager : MonoBehaviour
{
    private Ray ray;
    private Camera _camera;
    private RaycastHit hit;
    private int layerMask;
    private int layerMask2;

    private GameObject _tower;

    private void Awake()
    {
        _camera = Camera.main;
        layerMask = LayerMask.GetMask("Slime");
        layerMask2 = LayerMask.GetMask("Water");
    }

    private void Update()
    {
        ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (_tower != hit.collider.gameObject)
                {
                    _tower = hit.collider.gameObject;
                }
            }
        }

        if (Input.GetMouseButton(0) && _tower != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask2))
            {
                _tower.transform.position = hit.point;
            }
        }


        if (Input.GetMouseButtonUp(0) && _tower != null)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask2))
            {
                _tower.transform.position = hit.point;
                _tower = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_camera != null)
        {
            ray = _camera.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * 100);
        }
    }
}