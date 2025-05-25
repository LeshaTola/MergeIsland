using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements.Items.Systems;
using UnityEngine;

namespace App.Scripts.Features.Merge.Factory
{
    public class ItemConfigsFactory
    {
        private readonly ItemConfigsFactoryConfig _config;
        private readonly ItemSystemsFactory _itemSystemsFactory;

        public ItemConfigsFactory(ItemSystemsFactory itemSystemsFactory, ItemConfigsFactoryConfig config)
        {
            _itemSystemsFactory = itemSystemsFactory;
            _config = config;
        }

        public ItemConfig GetConfig(ItemConfig original, bool isInWeb = false)
        {
            if (original == null)
            {
                return original;
            }

            var newConfig = Object.Instantiate(original);
            var system = GetSystem(original, isInWeb);
            newConfig.Initialize(system);
            return newConfig;
        }

        private ItemSystem GetSystem(ItemConfig original, bool isInWeb)
        {
            return isInWeb
                ? _itemSystemsFactory.GetSystem(_config.WebItemSystem)
                : _itemSystemsFactory.GetSystem(original.System);
        }
    }
}