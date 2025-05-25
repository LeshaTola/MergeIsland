using App.Scripts.Features.Merge.Screens;
using App.Scripts.Features.OverlayItemAnimators;
using App.Scripts.Features.SellBuy.Configs;
using App.Scripts.Features.SellBuy.Services;
using App.Scripts.Modules.PopupAndViews;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Features.Merge.Elements.Items.Actions
{
    public class SellAction : ItemSystemAction
    {
        private readonly SellBuyService _sellBuyService;
        private readonly OverlayItemAnimator _overlayItemAnimator;

        public SellAction(SellBuyService sellBuyService, OverlayItemAnimator overlayItemAnimator)
        {
            _sellBuyService = sellBuyService;
            _overlayItemAnimator = overlayItemAnimator;
        }

        public override async void Execute()
        {
            Item.Release();
            var cost = _sellBuyService.GetSellCost(Item.Config);
            await Animate(cost);
            _sellBuyService.Sell(cost);
        }

        public override ActionData GetActionData()
        {
            return new ActionData
            {
                ActionText = ConstStrings.SELL,
                ActionImage = _sellBuyService.GetSellCost(Item.Config).ResourceConfig.Sprite,
                ButtonText = _sellBuyService.GetSellCost(Item.Config).Count.ToString(),
            };
        }

        private async UniTask Animate(Cost cost)
        {
            await _overlayItemAnimator
                .MoveFromSellPosition(
                    cost.ResourceConfig.EndPosition,
                    cost.ResourceConfig.Sprite);
        }
    }
}