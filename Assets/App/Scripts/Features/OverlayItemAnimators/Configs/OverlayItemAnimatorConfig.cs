using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Features.OverlayItemAnimators.Configs
{
    [CreateAssetMenu(fileName = "OverlayItemAnimatorConfig", menuName = "Configs/OverlayItemAnimator")]
    public class OverlayItemAnimatorConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] public Ease MoveEase { get; private set; }
        [field: SerializeField] public float ScaleDuration { get; private set; }
        [field: SerializeField] public Ease StartScaleEase { get; private set; }
        [field: SerializeField] public Ease EndScaleEase { get; private set; }
        
        [field: SerializeField] public Vector2 SellPosition { get; private set; }
    }
}