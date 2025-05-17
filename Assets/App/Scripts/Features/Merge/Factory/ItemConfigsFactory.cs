using App.Scripts.Features.Merge.Configs;
using UnityEngine;

namespace App.Scripts.Features.Merge.Factory
{
    public class ItemConfigsFactory
    {
        private readonly ItemSystemsFactory _itemSystemsFactory;

        public ItemConfigsFactory(ItemSystemsFactory itemSystemsFactory)
        {
            _itemSystemsFactory = itemSystemsFactory;
        }

        public ItemConfig GetConfig(ItemConfig original)
        {
            if (original == null)
            {
                return original;
            }
            
            var newConfig = Object.Instantiate(original);
            newConfig.Initialize(_itemSystemsFactory.GetSystem(original.System));
            return newConfig;
        }
    }
}