﻿using System.Collections.Generic;
using App.Scripts.Modules.ObjectPool.MonoObjectPools;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using UnityEngine;

namespace App.Scripts.Modules.ObjectPool.KeyPools
{
    public class KeyPool<T>
        where T : MonoBehaviour
    {
        private Dictionary<string, IPool<T>> poolsDictionary = new();

        public KeyPool(Dictionary<string, PoolObject<T>> pooledObjects, Transform container)
        {
            foreach (var key in pooledObjects.Keys)
            {
                var pool = new MonoObjectPool<T>(
                    pooledObjects[key].Template,
                    pooledObjects[key].PreloadCount,
                    container
                );
                poolsDictionary.Add(key, pool);
            }
        }

        public void AddPool(string key, IPool<T> pool)
        {
            if (poolsDictionary.ContainsKey(key))
            {
                Debug.LogError("Pool with such key is already exist");
                return;
            }

            poolsDictionary.Add(key, pool);
        }

        public T Get(string key)
        {
            if (poolsDictionary.TryGetValue(key, out var correctPool))
            {
                return correctPool.Get();
            }

            Debug.LogError($"There is no such pool key: {key}");
            return null;
        }

        public void Release(string key, T pooledObject)
        {
            if (poolsDictionary.TryGetValue(key, out var correctPool))
            {
                correctPool.Release(pooledObject);
            }

            Debug.LogError($"There is no such pool key: {key}");
        }
    }
}