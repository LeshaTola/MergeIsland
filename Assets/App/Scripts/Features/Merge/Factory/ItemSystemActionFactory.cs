using App.Scripts.Features.Merge.Elements.Items.Actions;
using Zenject;

namespace App.Scripts.Features.Merge.Factory
{
    public class ItemSystemActionFactory
    {
        private readonly DiContainer _container;

        public ItemSystemActionFactory(DiContainer container)
        {
            _container = container;
        }

        public ItemSystemAction GetAction(ItemSystemAction original)
        {
            if (original == null)
            {
                return null;
            }

            var action = (ItemSystemAction) _container.Instantiate(original.GetType());
            action.Import(original);
            return action;
        }

        public T GetAction<T>() where T : ItemSystemAction
        {
            return (T) _container.Instantiate(typeof(T));
        }
    }
}