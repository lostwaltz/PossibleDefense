using System.Collections.Generic;
using System.Runtime.InteropServices;
using Achievement;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            var queue = new Queue<GameObject>();
            for (var i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            _poolDictionary.Add(pool.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string poolTag)
    {
        if(!_poolDictionary.ContainsKey(poolTag)) return null;

        GameObject obj = _poolDictionary[poolTag].Dequeue();
        _poolDictionary[poolTag].Enqueue(obj);

        obj.SetActive(true);
        return obj;
    }
}
