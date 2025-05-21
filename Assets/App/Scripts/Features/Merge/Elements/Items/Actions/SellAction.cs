using App.Scripts.Features.Merge.Screens;
using App.Scripts.Modules.PopupAndViews;
using UnityEngine;

namespace App.Scripts.Features.Merge.Elements.Items.Actions
{
    public class SellAction : ItemSystemAction
    {
        public override void Execute()
        {
            Debug.Log("Sell");
        }

        public override ActionData GetActionData()
        {
            return new ActionData
            {
                ActionText = ConstStrings.SELL,
                ActionImage = null,
                ButtonText = ConstStrings.SELL
            };
        }
    }
}