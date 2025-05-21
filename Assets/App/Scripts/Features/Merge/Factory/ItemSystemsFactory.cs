using App.Scripts.Features.Merge.Elements.Items.Systems;
using Zenject;

namespace App.Scripts.Features.Merge.Factory
{
    public class ItemSystemsFactory
    {
        private readonly DiContainer _container;
        private readonly ItemSystemActionFactory _actionFactory;

        public ItemSystemsFactory(DiContainer container, ItemSystemActionFactory actionFactory)
        {
            _container = container;
            _actionFactory = actionFactory;
        }

        public ItemSystem GetSystem(ItemSystem original)
        {
            if (original == null)
            {
                return null;
            }

            var system = (ItemSystem) _container.Instantiate(original.GetType());
            system.Action = _actionFactory.GetAction(original.Action);
            system.Import(original);
            return system;
        }

        public ItemSystem GetSystem<T>() where T : ItemSystem
        {
            var system = (ItemSystem) _container.Instantiate(typeof(T));
            return system;
        }
    }
}