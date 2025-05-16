using App.Scripts.Features.Merge.Configs;

namespace App.Scripts.Features.Merge.Services
{
    public class MergeResolver
    {
        private CatalogsDatabase _catalogsDatabase;

        public bool TryMerge(string id, out MergeItemConfig config)
        {
            foreach (var catalogConfig in _catalogsDatabase.Database)
            {
                if (!catalogConfig.Value.IsInCatalog(id))
                {
                    continue;
                }

                var nextLevelConfig = catalogConfig.Value.GetNextLevelConfig(id);
                config = nextLevelConfig;
                return config != null;
            }

            config = null;
            return false;
        }
    }
}