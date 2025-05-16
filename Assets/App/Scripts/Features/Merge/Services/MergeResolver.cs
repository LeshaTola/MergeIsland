using App.Scripts.Features.Merge.Configs;
using App.Scripts.Features.Merge.Elements;

namespace App.Scripts.Features.Merge.Services
{
    public class MergeResolver
    {
        private readonly CatalogsDatabase _catalogsDatabase;

        public MergeResolver(CatalogsDatabase catalogsDatabase)
        {
            _catalogsDatabase = catalogsDatabase;
        }

        public bool TryMerge(Item firstItem, Item secondItem, out ItemConfig config)
        {
            if (!firstItem.Id.Equals(secondItem.Id))
            {
                config = null;
                return false;
            }

            config = GetNextLevel(firstItem);
            return config != null;
        }

        private ItemConfig GetNextLevel(Item firstItem)
        {
            var id = firstItem.Id;
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