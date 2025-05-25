using System.Threading.Tasks;
using App.Scripts.Features.GameResources.Providers;
using App.Scripts.Features.Merge.Screens;
using App.Scripts.Features.OverlayItemAnimators;
using App.Scripts.Features.SellBuy.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Systems
{
    public class AddResourceItemSystem : ItemSystem
    {
        [SerializeField] private Cost _cost;
        
        private readonly ResourcesProvider _resourcesProvider;
        private readonly OverlayItemAnimator _overlayItemAnimator;

        public AddResourceItemSystem(ResourcesProvider resourcesProvider, OverlayItemAnimator overlayItemAnimator)
        {
            _resourcesProvider = resourcesProvider;
            _overlayItemAnimator = overlayItemAnimator;
        }

        public override async void Execute()
        {
            Item.Release();
            await Animate();
            _resourcesProvider.Change(_cost.ResourceConfig, _cost.Count);
        }

        public override SystemData GetSystemData()
        {
            var data = base.GetSystemData();
            data.Description = $"Tap to get {_cost.Count} {_cost.ResourceConfig.name}. " + data.Description; 
            return data;
        }

        public override void Import(ItemSystem original)
        {
            var concrete = (AddResourceItemSystem) original;
            _cost = concrete._cost;
        }

        private async UniTask Animate()
        {
            await _overlayItemAnimator.Move(Item.transform.position, _cost.ResourceConfig.EndPosition ,_cost.ResourceConfig.Sprite);
        }
    }
}