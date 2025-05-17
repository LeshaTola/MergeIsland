using System.Collections.Generic;
using App.Scripts.Modules.ObjectPool.Pools;
using UnityEngine;

namespace App.Scripts.Modules.ObjectPool.MonoObjectPools
{
    public class MonoObjectPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _core;

        public IReadOnlyCollection<T> Active => _core.Active;

        public MonoObjectPool(T objectTemplate, int startCount, Transform parent = null)
        {
            _core = new ObjectPool<T>(
                () =>
                {
                    var pooledObject = Object.Instantiate(objectTemplate, parent);
                    pooledObject.gameObject.SetActive(false);
                    return pooledObject;
                },
                (obj) => { obj.gameObject.SetActive(true); },
                (obj) =>
                {
                    obj.gameObject.SetActive(false);
                    if (parent != null)
                    {
                        obj.transform.SetParent(parent);
                    }
                },
                startCount
            );
        }

        public T Get()
        {
            var pooledObject = _core.Get();
            return pooledObject;
        }

        public void Release(T pooledObject)
        {
            _core.Release(pooledObject);
        }
    }
}