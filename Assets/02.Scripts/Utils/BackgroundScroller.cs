using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f; // 스크롤 속도
    private Material _material;
    private Vector2 _offset;

    private void Start()
    {
        _material = GetComponent<Material>();
        _offset = new Vector2(0, scrollSpeed);
    }

    private void Update()
    {
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }
}