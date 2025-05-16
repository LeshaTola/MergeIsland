using App.Scripts.Modules.StateMachine;
using App.Scripts.Modules.StateMachine.Factories.States;
using App.Scripts.Modules.StateMachine.States.General;
using App.Scripts.Scenes.Gameplay.StateMachines.States;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class StateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStatesFactory();
            BindStateMachine();

            BindInitialState();
            Container.Bind<State>().To<MainState>().AsSingle();
        }

        private void BindInitialState()
        {
            Container.Bind<State>().To<InitialState>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<StateMachine>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
        }
    }
}