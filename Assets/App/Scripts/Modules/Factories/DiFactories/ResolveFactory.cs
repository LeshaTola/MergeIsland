using Zenject;

namespace App.Scripts.Modules.Factories.DiFactories
{
    public class ResolveFactory<T> : IFactory<T>
    {
        private DiContainer _diContainer;

        public ResolveFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T GetItem()
        {
            return _diContainer.Resolve<T>();
        }
    }
}