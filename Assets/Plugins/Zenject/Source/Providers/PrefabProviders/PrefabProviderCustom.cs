#if !NOT_UNITY3D

using System;
using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public class PrefabProviderCustom : IPrefabProvider
    {
        private readonly Func<InjectContext, UnityEngine.Object> _getter;

        public PrefabProviderCustom(Func<InjectContext, UnityEngine.Object> getter)
        {
            _getter = getter;
        }

        public UnityEngine.Object GetPrefab(InjectContext context)
        {
            var prefab = _getter(context);
            Assert.That(prefab != null, "Custom prefab provider returned null");
            return prefab;
        }
    }
}

#endif