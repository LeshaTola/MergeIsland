using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements;
using App.Scripts.Features.Merge.Elements.Items;
using App.Scripts.Features.Merge.Factory;

namespace App.Scripts.Features.Merge.Services
{
    public class MergeResolver
    {
        private readonly CatalogsDatabase _catalogsDatabase;
        private readonly ItemConfigsFactory _itemConfigsFactory;

        public MergeResolver(CatalogsDatabase catalogsDatabase, ItemConfigsFactory itemConfigsFactory)
        {
            _catalogsDatabase = catalogsDatabase;
            _itemConfigsFactory = itemConfigsFactory;
        }

        public bool TryMerge(Item firstItem, Item secondItem, out ItemConfig config)
        {
            if (!firstItem.Config.Id.Equals(secondItem.Config.Id))
            {
                config = null;
                return false;
            }

            config = _itemConfigsFactory.GetConfig(GetNextLevel(firstItem));
            return config != null;
        }

        private ItemConfig GetNextLevel(Item firstItem)
        {
            var id = firstItem.Config.Id;
            foreach (var catalogConfig in _catalogsDatabase.Database)
            {
                if (!catalogConfig.Value.IsInCatalog(id))
                {
                    continue;
                }

                return catalogConfig.Value.GetNextLevelConfig(id);
            }
            return null;
        }
    }
}