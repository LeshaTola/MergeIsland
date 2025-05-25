using System.Threading.Tasks;
using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Factory;
using App.Scripts.Features.Merge.Screens;
using App.Scripts.Features.OverlayItemAnimators;
using App.Scripts.Features.SellBuy.Configs;
using App.Scripts.Features.SellBuy.Services;
using App.Scripts.Modules.PopupAndViews;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Actions
{
    public class UnlockAction : ItemSystemAction
    {
        [SerializeField] private Cost _cost;
        
        private readonly SellBuyService _sellBuyService;
        private readonly CatalogsDatabase _catalogsDatabase;
        private readonly ItemConfigsFactory _itemConfigsFactory;
        private readonly OverlayItemAnimator _overlayItemAnimator;

        public UnlockAction(SellBuyService sellBuyService,
            CatalogsDatabase catalogsDatabase,
            ItemConfigsFactory itemConfigsFactory,
            OverlayItemAnimator overlayItemAnimator)
        {
            _sellBuyService = sellBuyService;
            _catalogsDatabase = catalogsDatabase;
            _itemConfigsFactory = itemConfigsFactory;
            _overlayItemAnimator = overlayItemAnimator;
        }

        public override async void Execute()
        {
            if (!_sellBuyService.TryBuy(_cost))
            {
                Debug.LogError($"Failed to buy cost: {_cost.Count}");
                return;
            }
            Item.Setup(GetNewConfig());
            await Animate();
        }

        public override void Import(ItemSystemAction original)
        {
            var concrete = (UnlockAction)original;
            _cost = concrete._cost;
        }

        public override ActionData GetActionData()
        {
            return new ActionData
            {
                ActionText = ConstStrings.SELL,
                ActionImage = _cost.ResourceConfig.Sprite,
                ButtonText = _cost.Count.ToString(),
            };
        }

        private ItemConfig GetNewConfig()
        {
            var original = _catalogsDatabase.GetItemConfig(Item.Config.Id);
            var itemConfig = _itemConfigsFactory.GetConfig(original);
            return itemConfig;
        }

        private async UniTask Animate()
        {
            await _overlayItemAnimator.MoveToSellPosition(_cost.ResourceConfig.EndPosition, _cost.ResourceConfig.Sprite);
        }
    }
}