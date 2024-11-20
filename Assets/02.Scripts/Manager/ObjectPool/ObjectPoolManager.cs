using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private readonly Dictionary<string, IObjectPoolWrapper<Component>> _poolDictionary = new();

    private interface IObjectPoolWrapper<out T>
    {
        T Get();
        void Release(Component obj);
    }

    private class ObjectPoolWrapper<T> : IObjectPoolWrapper<T> where T : Component
    {
        private readonly ObjectPool<T> _objectPool;
        
        public ObjectPoolWrapper(Func<T> createFunc, Action<T> actionOnGet, Action<T> actionOnRelease, Action<T> actionOnDestroy, int defaultCapacity, int maxSize)
        {
            _objectPool = new ObjectPool<T>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, false, defaultCapacity, maxSize);
        }

        public T Get()
        {
            return _objectPool.Get();
        }

        public void Release(Component obj)
        {
            if (false == (obj is T tObj))
                return;
            
            _objectPool.Release(tObj);
        }
    }

    public void CreateNewPool<T>(string key, T pooledObject, int capacity, int maxSize) where T : Component
    {
        if (_poolDictionary.ContainsKey(key))
            return;

        var poolWrapper = new ObjectPoolWrapper<T>(
            () => CreateInstance(pooledObject),
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            capacity,
            maxSize
        );

        _poolDictionary[key] = poolWrapper;

        for(var i = 0; i < capacity; i++)
            poolWrapper.Release(Instantiate(pooledObject, transform));
    }

    public T GetPooledObject<T>(string key) where T : Component
    {
        if (!_poolDictionary.TryGetValue(key, out var poolWrapper))
            return null;

        T component = poolWrapper.Get() as T;

        return component;
    }

    public void ReleaseObject<T>(string key, T poolObject) where T : Component
    {
        if (!_poolDictionary.TryGetValue(key, out var poolWrapper))
            return;

        poolObject.transform.SetParent(this.transform, false);
        poolWrapper.Release(poolObject);
    }

    private static T CreateInstance<T>(T pooledObject) where T : Component
    {
        T component = Instantiate(pooledObject);
        component.gameObject.SetActive(false);
        return component;
    }

    private static void OnGetFromPool(Component component)
    {
        component.gameObject.SetActive(true);
    }

    private static void OnReleaseToPool(Component component)
    {
        component.gameObject.SetActive(false);
    }

    private static void OnDestroyPooledObject(Component component)
    {
        Destroy(component.gameObject);
    }
}
