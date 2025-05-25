using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.GameResources.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/Resources/Resource")]
    public class ResourceConfig : ScriptableObject
    {
        [field: SerializeField, ReadOnly] public string Id { get; private set; } 
        [field: SerializeField] public Sprite Sprite { get; private set; } 
        
        [field: SerializeField] public Vector2 EndPosition { get; private set; } 

        private void OnValidate()
        {
            Id = name;
        }
    }
}