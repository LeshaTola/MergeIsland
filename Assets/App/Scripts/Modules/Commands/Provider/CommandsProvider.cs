using App.Scripts.Modules.Commands.General;
using Zenject;

namespace App.Scripts.Modules.Commands.Provider
{
    public class CommandsProvider : ICommandsProvider
    {
        private readonly DiContainer _diContainer;

        public CommandsProvider(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T GetCommand<T>() where T : ICommand
        {
            return (T) _diContainer.Resolve(typeof(T));
        }
    }
}